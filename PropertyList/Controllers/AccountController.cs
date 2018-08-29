using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PropertyList.Helper;
using PropertyList.Models;
using PropertyList.BusinessLogic.Model;


namespace PropertyList.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationUriResolver _applicationUriResolver;

        public AccountController() : this(new ApplicationUriResolver())
        {
        }

        public AccountController(IApplicationUriResolver applicationUriResolver)
        {
            _applicationUriResolver = applicationUriResolver;
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

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

                    Session["UserID"] = result.StaffID;
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

        public ActionResult Logout() //todo
        {
            Session["UserID"] = null;
            Session["Username"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}