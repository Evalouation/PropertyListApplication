using Moq;
using NUnit.Framework;
using PropertyList.BusinessLogic.Providers;
using PropertyList.Data.Model;
using PropertyList.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace PropertyList.BusinessLogic.Tests
{
    [TestFixture]
    public class PropertyProviderTest
    {
        protected Mock<IUnitOfWork> mockUnitOfWork = null;

        [SetUp]
        public void Initialize()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();               
        }

        [Test]
        public void UnitTest_GetAll_Returns_ListOfProperties()
        {
            var dummyData = new List<usp_GetAllProperties_Result> {
                new usp_GetAllProperties_Result() { PropertyID = 1, Location = "111 street" ,Bedroom = 3, Bathroom = 2, ConfidentialNotes = null, Status = 1 },
                new usp_GetAllProperties_Result() { PropertyID = 2, Location = "222 street" ,Bedroom = 3, Bathroom = 1, ConfidentialNotes = null, Status = 2 },
                new usp_GetAllProperties_Result() { PropertyID = 3, Location = "333 street" ,Bedroom = 5, Bathroom = 3, ConfidentialNotes = null, Status = 4 }
            };

            var mockedObjectResult = new Mock<ObjectResult<usp_GetAllProperties_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_GetAllProperties()).Returns(mockedObjectResult.Object);
            PropertyProvider provider = new PropertyProvider(mockUnitOfWork.Object);

            var result = provider.GetAll();

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void UnitTest_GetByPropertyId_Returns_FoundProperty()
        {
            var dummyData = new List<usp_GetPropertyById_Result> {
                new usp_GetPropertyById_Result() { PropertyID = 1, Location = "111 street" ,Bedroom = 3, Bathroom = 2, ConfidentialNotes = null, Status = 1 },
            };

            var mockedObjectResult = new Mock<ObjectResult<usp_GetPropertyById_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_GetPropertyById(1)).Returns(mockedObjectResult.Object);
            PropertyProvider provider = new PropertyProvider(mockUnitOfWork.Object);

            var result = provider.GetById(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void UnitTest_GetByPropertyId_Returns_Empty()
        {
            var dummyData = new List<usp_GetPropertyById_Result> { };

            var mockedObjectResult = new Mock<ObjectResult<usp_GetPropertyById_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_GetPropertyById(3)).Returns(mockedObjectResult.Object);
            PropertyProvider provider = new PropertyProvider(mockUnitOfWork.Object);

            var result = provider.GetById(3);
            Assert.IsNull(result);
        }

        [Test]
        public void UnitTest_CreateNewProperty()
        {
            var dummyData = new List<usp_GetPropertyById_Result> {
                new usp_GetPropertyById_Result() { PropertyID = 1, Location = "111 street" ,Bedroom = 2, Bathroom = 1, ConfidentialNotes = null, Status = 1 },
            };

            var mockedObjectResult = new Mock<ObjectResult<usp_GetPropertyById_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_GetPropertyById(1)).Returns(mockedObjectResult.Object);
            mockUnitOfWork.Setup(x => x.GetDB().usp_InsertSingleProperty("111 street", 2, 1, null, 1, false, DateTime.Now, DateTime.Now));

            PropertyProvider provider = new PropertyProvider(mockUnitOfWork.Object);
            var m = provider.CreateProperty(new Model.PropertyDtoModel { PropertyID = 1, Location = "111 street", Bedroom = 2, Bathroom = 1, ConfidentialNotes = null, Status = 1 });
            var result = provider.GetById(m.PropertyID);

            Assert.IsNotNull(result);
        }

        [Test]
        public void UnitTest_UpdateExistingProperty()
        {
            var dummyData = new List<usp_GetPropertyById_Result> {
                new usp_GetPropertyById_Result() { PropertyID = 1, Location = "123 street" ,Bedroom = 2, Bathroom = 1, ConfidentialNotes = null, Status = 1 },
            };

            var mockedObjectResult = new Mock<ObjectResult<usp_GetPropertyById_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_InsertSingleProperty("111 street", 2, 1, null, 1, false, DateTime.Now, DateTime.Now));
            mockUnitOfWork.Setup(x => x.GetDB().usp_UpdateProperty(1, "123 street", 2, 1, null, 1, false, DateTime.Now));
            mockUnitOfWork.Setup(x => x.GetDB().usp_GetPropertyById(1)).Returns(mockedObjectResult.Object);

            PropertyProvider provider = new PropertyProvider(mockUnitOfWork.Object);
            var newProerty = provider.CreateProperty(new Model.PropertyDtoModel { PropertyID = 1, Location = "111 street", Bedroom = 2, Bathroom = 1, ConfidentialNotes = null, Status = 1 });
            var updatedModel = provider.UpdateProperty(new Model.PropertyDtoModel { PropertyID = 1, Location = "123 street", Bedroom = 2, Bathroom = 1, ConfidentialNotes = null, Status = 1 });

            var result = provider.GetById(updatedModel.PropertyID);
            Assert.AreEqual("123 street", result.Location);
        }
    }
}
