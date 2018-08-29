using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PropertyList.Models;

namespace PropertyList.Controllers
{
    public class HomeController : Controller
    {
        string baseurl = "http://propertylist.localhost/";

        public async Task<ActionResult> Index()
        {
            List<PropertyViewModel> result = new List<PropertyViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
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

        public ActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStaff(StaffViewModel staff)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/StaffApi/Post", staff);

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