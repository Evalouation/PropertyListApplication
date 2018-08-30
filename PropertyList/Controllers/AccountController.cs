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

namespace PropertyList.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationUriResolver _applicationUriResolver;

        #region injection
        public AccountController() : this(new ApplicationUriResolver())
        {
        }

        public AccountController(IApplicationUriResolver applicationUriResolver)
        {
            _applicationUriResolver = applicationUriResolver;
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/StaffApi/ValidateAccount", user);
                if (response.IsSuccessStatusCode)
                {
                    var propertyResponse = response.Content.ReadAsStringAsync().Result;
                    UserDtoModel result = JsonConvert.DeserializeObject<UserDtoModel>(propertyResponse);

                    string userData = JsonConvert.SerializeObject(result);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1, result.FirstName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                    );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("CookieA", enTicket);
                    Response.Cookies.Add(faCookie);

                    Session["Username"] = result.FirstName;
                    return RedirectToAction("Index", "Property");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }
            }

            return View(user);
        }

        //GET: Account/Logout
        public ActionResult Logout()
        {
            Session["Username"] = null;
            FormsAuthentication.SignOut();
            CleanCookie();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/CreateStaff
        [AuthorizeRoles("Admin")]
        [HttpGet]
        public ActionResult CreateStaff()
        {
            return View();
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


        private void CleanCookie()
        {
            HttpCookie currentUserCookie = HttpContext.Request.Cookies["CookieA"];
            HttpContext.Response.Cookies.Remove("CookieA");
            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
            currentUserCookie.Value = null;
            HttpContext.Response.SetCookie(currentUserCookie);
        }
    }
}