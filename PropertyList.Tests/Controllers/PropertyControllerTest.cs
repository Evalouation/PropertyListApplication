using Moq;
using NUnit.Framework;
using PropertyList.BusinessLogic.Constant;
using PropertyList.Controllers;
using PropertyList.Helper;
using PropertyList.Tests.Helper;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace PropertyList.Tests.Controllers
{
    [TestFixture]
    public class PropertyControllerTest
    {
        protected Mock<IApplicationUriResolver> mockAppUriResolver = null;
        protected Mock<IUserAccountResolver> mockUserAccountResolver = null;

        [SetUp]
        public void Initialize()
        {
            mockUserAccountResolver = new Mock<IUserAccountResolver>();
            mockAppUriResolver = new Mock<IApplicationUriResolver>();
            mockAppUriResolver.Setup(x => x.GetBaseUrl()).Returns("http://localhost");        
        }

        [Test]
        public void Verify_IndexAction_with_Role_Sales_Returns_PropertyListForSales_View()
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)StaffType.IsSales);

            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var resultTask = controller.Index();
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("PropertyListForSales", viewResult.ViewName);
        }

        [Test]
        public void Verify_IndexAction_with_Role_SalesAdmin_Returns_PropertyListForSalesAdmin_View()
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)StaffType.IsSalesAdmin);

            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var resultTask = controller.Index();
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("PropertyListForSalesAdmin", viewResult.ViewName);
        }

        [Test]
        public void Verify_IndexAction_with_Role_SalesDepartmentAdmin_Returns_PropertyListForSalesDepartmentAdmin_View()
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)StaffType.IsSalesDepartmentAdmin);

            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var resultTask = controller.Index();
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("PropertyListForSalesDepartmentAdmin", viewResult.ViewName);
        }

        [TestCase(null)] //public user
        [TestCase(StaffType.Admin)]     
        public void Verify_IndexAction_with_Role_Admin_Returns_PropertyListReadOnly_View(StaffType staffType)
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)staffType);

            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var resultTask = controller.Index();
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("PropertyListReadOnly", viewResult.ViewName);
        }


        [Test]
        public void Verify_EditAction_MustHaveAuthorizeRolesAttribute()
        {
            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var hasAttribute = TestHelper.MethodHasAuthorizeAttribute(() => controller.Edit(2));
            Assert.IsTrue(hasAttribute);
        }


        [TestCase(StaffType.IsSales)]
        public void Verify_EditAction_with_Role_Sales_Returns_Edit_View(StaffType staffType)
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)staffType);

            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var resultTask = controller.Edit(2);
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        [TestCase(StaffType.IsSalesAdmin)]
        [TestCase(StaffType.IsSalesDepartmentAdmin)]
        public void Verify_EditAction_with_Role_SalesAdmin_Returns_EditSalesAdmin_View(StaffType staffType)
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)staffType);
            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);

            var resultTask = controller.Edit(3);
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("EditSalesAdmin", viewResult.ViewName);
        }

        [Test]
        public void Verify_CreateAction_MustHaveAuthorizeRolesAttribute()
        {
            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var hasAttribute = TestHelper.MethodHasAuthorizeAttribute(() => controller.Create());
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Verify_CreateAction_Only_Role_Sales_Returns_Create_View()
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)StaffType.IsSales);

            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var viewResult = controller.Create() as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Create", viewResult.ViewName);
        }


        [Test]
        public void Verify_DeleteAction_MustHaveAuthorizeRolesAttribute()
        {
            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var hasAttribute = TestHelper.MethodHasAuthorizeAttribute(() => controller.Delete(2));
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Verify_DeleteAction_Only_Role_SalesDepartmentAdmin_Can_Execute()
        {
            mockUserAccountResolver.Setup(x => x.GetCurrentUserRole()).Returns((int)StaffType.IsSalesDepartmentAdmin);

            PropertyController controller = new PropertyController(mockAppUriResolver.Object, mockUserAccountResolver.Object);
            var resultTask = controller.Delete(3);
            resultTask.Wait();
            var viewResult = resultTask.Result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("EditSalesAdmin", viewResult.ViewName);
        }

        //public static MethodInfo MethodOf(Expression<System.Action> expression)
        //{
        //    var body = (MethodCallExpression)expression.Body;
        //    return body.Method;
        //}

        //public static bool MethodHasAuthorizeAttribute(Expression<System.Action> expression)
        //{
        //    var method = MethodOf(expression);
        //    const bool includeInherited = false;
        //    return method.GetCustomAttributes(typeof(AuthorizeAttribute), includeInherited).Any();
        //}
    }
}
