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
using PropertyList.Attributes;
using PropertyList.BusinessLogic.Constant;

namespace PropertyList.Controllers
{

    public class PropertyController : Controller
    {
        private readonly IApplicationUriResolver _applicationUriResolver;
        private readonly IUserAccountResolver _userAccountResolver;

        #region injection
        public PropertyController() : this(new ApplicationUriResolver(), new UserAccountResolver())
        {
        }

        public PropertyController(IApplicationUriResolver applicationUriResolver, IUserAccountResolver userAccountResolver)
        {
            _applicationUriResolver = applicationUriResolver;
            _userAccountResolver = userAccountResolver;
        }
        #endregion

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
            }

            return RenderPropertyListView(result);
        }

        // GET: Property/Create
        [AuthorizeRoles("IsSales")]
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Property/Create
        [AuthorizeRoles("IsSales")]
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
        [AuthorizeRoles("IsSales", "IsSalesAdmin", "IsSalesDepartmentAdmin")]
        [HttpGet]
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
                return RenderPropertyEditView(result);
            }
        }

        //POST: Property/Edit/5
        [AuthorizeRoles("IsSales", "IsSalesAdmin", "IsSalesDepartmentAdmin")]
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

        // DELETE: Property/Delete/5
        [HttpPost]
        [AuthorizeRoles("IsSalesDepartmentAdmin")]
        public async Task<ActionResult> Delete(int? id)
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

                HttpResponseMessage response = await client.DeleteAsync("api/PropertyApi/Delete/" + id.Value);

                if (response.IsSuccessStatusCode)
                {
                    var propertyResponse = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<PropertyViewModel>(propertyResponse);
                }
                return RenderPropertyEditView(result);
            }
        }

        private ViewResult RenderPropertyListView(IEnumerable<PropertyViewModel> model)
        {
            int role = _userAccountResolver.GetCurrentUserRole();
  
            if (role == (int)StaffType.IsSales)
                return View("PropertyListForSales", model);
            else if (role == (int)StaffType.IsSalesAdmin)
                return View("PropertyListForSalesAdmin", model);
            else if (role == (int)StaffType.IsSalesDepartmentAdmin)
                return View("PropertyListForSalesDepartmentAdmin", model);
            else
                return View("PropertyListReadOnly", model);
        }

        private ViewResult RenderPropertyEditView(PropertyViewModel model)
        {
            int role = _userAccountResolver.GetCurrentUserRole();

            if (role == (int)StaffType.IsSalesAdmin || role == (int)StaffType.IsSalesDepartmentAdmin)
                return View("EditSalesAdmin", model);         
            else
                return View("Edit", model);
        }
    }
}