using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.Helper
{
    public class UserAccountResolver : IUserAccountResolver
    {
        public UserDtoModel GetCurrentUser()
        {
            UserDtoModel user = null;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["CookieA"];
            if (cookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                if (authTicket != null)
                {
                    user = JsonConvert.DeserializeObject<UserDtoModel>(authTicket.UserData);
                }
            }
            return user;
        }

        public int GetCurrentUserRole()
        {
            UserDtoModel user = GetCurrentUser();
            return user != null ? user.RoleID : 0;
        }
    }
}