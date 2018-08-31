using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace PropertyList.Tests.Helper
{
    public class TestHelper
    {
        public static MethodInfo MethodOf(Expression<System.Action> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }

        public static bool MethodHasAuthorizeAttribute(Expression<System.Action> expression)
        {
            var method = MethodOf(expression);
            const bool includeInherited = false;
            return method.GetCustomAttributes(typeof(AuthorizeAttribute), includeInherited).Any();
        }
    }
}
