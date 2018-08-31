using Moq;
using NUnit.Framework;
using PropertyList.Controllers;
using PropertyList.Helper;
using System.Web.Mvc;

namespace PropertyList.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void VerifyIndexActionReturnsIndexView()
        {
            var mockApplicationUriResolver = new Mock<IApplicationUriResolver>();
            mockApplicationUriResolver.Setup(x => x.GetBaseUrl()).Returns("http://localhost");
            var controller = new HomeController(mockApplicationUriResolver.Object);
            var resultTask = controller.Index();
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [Test]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
