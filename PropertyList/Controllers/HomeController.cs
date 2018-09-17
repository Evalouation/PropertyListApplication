using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PropertyList.Attributes;
using PropertyList.BusinessLogic.Constant;
using PropertyList.BusinessLogic.Model;
using PropertyList.BusinessLogic.Providers;
using PropertyList.Helper;
using PropertyList.Models;

namespace PropertyList.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationUriResolver _applicationUriResolver;
        private readonly IPropertyProvider _propertyProvider;

        #region injection
        public HomeController(IPropertyProvider propertyProvider) : this(new ApplicationUriResolver(), propertyProvider)
        {
        }

        public HomeController(IApplicationUriResolver applicationUriResolver, IPropertyProvider propertyProvider)
        {
            _applicationUriResolver = applicationUriResolver;
            _propertyProvider = propertyProvider;
        }
        #endregion

        public async Task<ActionResult> Index()
        {
            // Again, don't use API calls for code in the same application. I'd shortened the code below
            List<PropertyDtoModel> result = _propertyProvider.GetAll()?.ToList(); // var is OK to use when the type is obvious

            // Would normally create a mapping class for this
            List<PropertyViewModel> properties = result.Select(s => new PropertyViewModel()
            {
                Bathroom = s.Bathroom,
                Bedroom = s.Bedroom,
                ConfidentialNotes = s.ConfidentialNotes,
                Location = s.Location,
                PropertyID = s.PropertyID,
                Status = (PropertyStatus)s.Status
            }).ToList();

            return View("Index", properties);

            //List<PropertyViewModel> result = new List<PropertyViewModel>();

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage response = await client.GetAsync("api/PropertyApi/GetAll");

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var propertyResponse = response.Content.ReadAsStringAsync().Result;
            //        result = JsonConvert.DeserializeObject<List<PropertyViewModel>>(propertyResponse);
            //    }
            //    return View("Index", result);
            //}
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