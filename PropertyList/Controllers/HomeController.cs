using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PropertyList.Attributes;
using PropertyList.Helper;
using PropertyList.Models;

namespace PropertyList.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationUriResolver _applicationUriResolver;

        #region injection
        public HomeController() : this(new ApplicationUriResolver())
        {
        }

        public HomeController(IApplicationUriResolver applicationUriResolver)
        {
            _applicationUriResolver = applicationUriResolver;
        }
        #endregion

        public async Task<ActionResult> Index()
        {
            List<PropertyViewModel> result = new List<PropertyViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/PropertyApi/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    var propertyResponse = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<List<PropertyViewModel>>(propertyResponse);
                }
                return View(result);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}