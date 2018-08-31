using Moq;
using NUnit.Framework;
using PropertyList.BusinessLogic.Providers;
using PropertyList.Data.Model;
using PropertyList.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyList.BusinessLogic.Tests
{
    [TestFixture]
    public class StaffProviderTest
    {
        protected Mock<IUnitOfWork> mockUnitOfWork = null;

        [SetUp]
        public void Initialize()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public void UnitTest_CreateNewStaff()
        {
            var dummyData = new List<usp_GetStaffByEmail_Result> {
                new usp_GetStaffByEmail_Result() { StaffID = 1, FirstName = "ABC", LastName = "S", Email = "abc@mail.com", RoleID = 3}
            };

            var mockedObjectResult = new Mock<ObjectResult<usp_GetStaffByEmail_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_GetStaffByEmail("abc@mail.com")).Returns(mockedObjectResult.Object);

            mockUnitOfWork.Setup(x => x.GetDB().usp_InsertSingleStaff("ABC", "S", "abc@mail.com", "pass", 3, DateTime.Now, DateTime.Now));

            StaffProvider provider = new StaffProvider(mockUnitOfWork.Object);
            var m = provider.CreateStaff(new Model.StaffDtoModel { FirstName = "ABC", LastName = "S", Email = "abc@mail.com", Password = "pass", Role = 3 });
            var result = provider.GetByEmail("abc@mail.com");

            Assert.IsNotNull(result);
        }

        [Test]
        public void UnitTest_ValidateAccount_By_EmailAndPassword_Account_Exist()
        {
            var dummyData = new List<usp_CheckStaffAccount_Result> {
                new usp_CheckStaffAccount_Result() { StaffID = 1, FirstName = "ABC", RoleID = 3}
            };

            var mockedObjectResult = new Mock<ObjectResult<usp_CheckStaffAccount_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_CheckStaffAccount("abc@mail.com", "pass")).Returns(mockedObjectResult.Object);

            StaffProvider provider = new StaffProvider(mockUnitOfWork.Object);
            var result = provider.ValidateStaffAccount(new Model.LoginDtoModel { Email = "abc@mail.com", Password = "pass" });

            Assert.IsNotNull(result);
            Assert.AreEqual("ABC", result.FirstName);
        }

        [Test]
        public void UnitTest_ValidateAccount_By_EmailAndPassword_Account_Not_Exist()
        {
            var dummyData = new List<usp_CheckStaffAccount_Result> {
                new usp_CheckStaffAccount_Result() { StaffID = 1, FirstName = "ABC", RoleID = 3}
            };

            var mockedObjectResult = new Mock<ObjectResult<usp_CheckStaffAccount_Result>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockUnitOfWork.Setup(x => x.GetDB().usp_CheckStaffAccount("abc@mail.com", "pass")).Returns(mockedObjectResult.Object);

            StaffProvider provider = new StaffProvider(mockUnitOfWork.Object);
            var result = provider.ValidateStaffAccount(new Model.LoginDtoModel { Email = "aaa@mail.com", Password = "pass" });

            Assert.IsNull(result);
        }
    }
}
