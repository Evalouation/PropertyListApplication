using Moq;
using NUnit.Framework;
using PropertyList.Controllers;
using PropertyList.Helper;
using PropertyList.Tests.Helper;
using System.Web.Mvc;

namespace PropertyList.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTest
    {

        protected Mock<IApplicationUriResolver> mockAppUriResolver = null;

        [SetUp]
        public void Initialize()
        {
            mockAppUriResolver = new Mock<IApplicationUriResolver>();
            mockAppUriResolver.Setup(x => x.GetBaseUrl()).Returns("http://localhost");
        }

        [Test]
        public void Verify_CreateStaff_Action_Must_Have_AuthorizeRoles_Attribute()
        {
            AccountController controller = new AccountController(mockAppUriResolver.Object);
            var hasAttribute = TestHelper.MethodHasAuthorizeAttribute(() => controller.CreateStaff());
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Verify_CreateStaff_Action_Only_Role_Admin_Returns_Create_View()
        {
            AccountController controller = new AccountController(mockAppUriResolver.Object);
            var viewResult = controller.CreateStaff() as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("CreateStaff", viewResult.ViewName);
        }
    }
}
