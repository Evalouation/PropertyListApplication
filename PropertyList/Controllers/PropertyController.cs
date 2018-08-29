using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PropertyList.Models;
using PropertyList.Helper;
using System.Collections.Generic;
using System.Net;

namespace PropertyList.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IApplicationUriResolver _applicationUriResolver;

        public PropertyController() : this(new ApplicationUriResolver())
        {
        }

        public PropertyController(IApplicationUriResolver applicationUriResolver)
        {
            _applicationUriResolver = applicationUriResolver;
        }


        // GET: Property
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

        // GET: Property/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PropertyViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/PropertyApi/Post", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var propertyResponse = response.Content.ReadAsStringAsync().Result;
                        //JsonConvert.DeserializeObject<StaffViewModel>(propertyResponse);
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        //GET: Property/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PropertyViewModel result = new PropertyViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/PropertyApi/Get/" + id.Value);

                if (response.IsSuccessStatusCode)
                {
                    var propertyResponse = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<PropertyViewModel>(propertyResponse);
                }
                return View(result);
            }
        }

        //POST: Property/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PropertyViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_applicationUriResolver.GetBaseUrl());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PutAsJsonAsync("api/PropertyApi/Put", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var propertyResponse = response.Content.ReadAsStringAsync().Result;
                        //JsonConvert.DeserializeObject<StaffViewModel>(propertyResponse);
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}