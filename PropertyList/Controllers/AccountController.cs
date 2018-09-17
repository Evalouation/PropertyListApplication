using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web;
using System.Web.Security;
using PropertyList.Helper;
using PropertyList.Models;
using PropertyList.BusinessLogic.Model;
using PropertyList.Attributes;
using PropertyList.BusinessLogic.Providers;

namespace PropertyList.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationUriResolver _applicationUriResolver;
        private readonly IStaffProvider _staffProvider;

        #region injection
        public AccountController(IStaffProvider staffProvider) : this(new ApplicationUriResolver(), staffProvider)
        {
        }

        public AccountController(IApplicationUriResolver applicationUriResolver, IStaffProvider staffProvider)
        {
            _applicationUriResolver = applicationUriResolver;
            _staffProvider = staffProvider;
        }
        #endregion

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel user)
        {
            // Shouldn't use API calls to code that lies in the same application. Just bypass the API and use the interafaces present
            // I've also bypassed the Facade. Could never understand why David used those :) They might work if you are creating an interface
            // that in turn wraps multiple classes, just to keep it tidy.
            var loginModel = new LoginDtoModel
            {
                Email = user.Email,
                Password = user.Password
            };
            UserDtoModel userModel = _staffProvider.ValidateStaffAccount(loginModel);
            if (userModel == null)
            {
                return View(user);
            }

            // Login and return
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, userModel.FirstName, DateTime.Now, DateTime.Now.AddMinutes(15), false, JsonConvert.SerializeObject(userModel));

            string enTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie enCookie = new HttpCookie("CookieA", enTicket);
            Response.Cookies.Add(enCookie);

            HttpCookie userCookie = new HttpCookie("Username");
            userCookie.Value = userModel.FirstName;
            userCookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(userCookie);

            return RedirectToAction("Index", "Property");



            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage response = await client.PostAsJsonAsync("api/StaffApi/ValidateAccount", user);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var propertyResponse = response.Content.ReadAsStringAsync().Result;
            //        UserDtoModel result = JsonConvert.DeserializeObject<UserDtoModel>(propertyResponse);

            //        string userData = JsonConvert.SerializeObject(result);
            //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
            //            1, result.FirstName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
            //        );

            //        string enTicket = FormsAuthentication.Encrypt(authTicket);
            //        HttpCookie enCookie = new HttpCookie("CookieA", enTicket);
            //        Response.Cookies.Add(enCookie);

            //        HttpCookie userCookie =  new HttpCookie("Username");
            //        userCookie.Value = result.FirstName;
            //        userCookie.Expires = DateTime.Now.AddHours(1);
            //        Response.Cookies.Add(userCookie);

            //        return RedirectToAction("Index", "Property");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Invalid credentials");
            //    }
            //}

            //return View(user);
        }

        //GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            CleanCookie("CookieA");
            CleanCookie("Username");
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/CreateStaff
        [AuthorizeRoles("Admin")]
        [HttpGet]
        public ActionResult CreateStaff()
        {
            return View("CreateStaff");
        }

        // POST: Account/CreateStaff
        [AuthorizeRoles("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStaff(StaffViewModel staff)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/StaffApi/Post", staff);

                    if (response.IsSuccessStatusCode)
                    {
                        var propertyResponse = response.Content.ReadAsStringAsync().Result;
                        //JsonConvert.DeserializeObject<StaffViewModel>(propertyResponse);
                    }

                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }


        private void CleanCookie(string cookieName)
        {
            // It's not such a big deal here, but in a normal application (and possibly a test liek this)
            // I'd wrap the below up into a class. I've added a demo class below to show what this might look like
            HttpCookie currentUserCookie = HttpContext.Request.Cookies[cookieName];
            HttpContext.Response.Cookies.Remove(cookieName);
            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
            currentUserCookie.Value = null;
            HttpContext.Response.SetCookie(currentUserCookie);

            // demo
            var helper = new DemoCookieHelper(HttpContext);
            helper.SetCookie(cookieName, TimeSpan.FromDays(-10), null);
        }
    }

    public class DemoCookieHelper
    {
        private readonly HttpContextBase _contextBase;

        public DemoCookieHelper(HttpContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public void SetCookie(string name, TimeSpan expireDays, string value)
        {
            ClearCookie(name);

            HttpCookie currentUserCookie = _contextBase.Request.Cookies[name];
            currentUserCookie.Expires = DateTime.Now - expireDays;
            currentUserCookie.Value = value;
            _contextBase.Response.SetCookie(currentUserCookie);
        }

        private void ClearCookie(string cookieName)
        {
            _contextBase.Response.Cookies.Remove(cookieName);
        }
    }
}