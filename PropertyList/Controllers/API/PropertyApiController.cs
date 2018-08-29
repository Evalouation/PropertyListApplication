using PropertyList.BusinessLogic.Model;
using PropertyList.ControllerLogic;
using PropertyList.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PropertyList.Controllers.API
{
    public class PropertyApiController : ApiController
    {
        private readonly IPropertyFacade _propertyFacade;

        public PropertyApiController() : this(new PropertyFacade())
        {
        }

        public PropertyApiController(IPropertyFacade propertyFacade)
        {
            _propertyFacade = propertyFacade;
        }


        // GET: api/PropertyApi/GetAll
        public IHttpActionResult GetAll()
        {
            var result = _propertyFacade.GetAllProperties();

            return Ok(result);
        }

        // GET: api/PropertyApi/Get/5
        public IHttpActionResult Get(int id)
        {
            var result = _propertyFacade.GetById(id);

            return Ok(result);
        }

        // POST: api/PropertyApi/Post
        [HttpPost]
        public IHttpActionResult Post(PropertyDtoModel property)
        {
            _propertyFacade.AddProperty(property);
            return Ok(property);
        }

        // PUT: api/PropertyApi/Put
        [HttpPut]
        public IHttpActionResult Put(PropertyDtoModel property)
        {
            _propertyFacade.UpdateProperty(property);
            return Ok(property);
        }

        // DELETE: api/PropertyApi/5
        public void Delete(int id)
        {
        }
    }
}
