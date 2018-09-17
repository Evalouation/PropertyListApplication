using System.Web.Http;
using PropertyList.Attributes;
using PropertyList.BusinessLogic.Model;
using PropertyList.ControllerLogic;

namespace PropertyList.Controllers.API
{
    [AuthorizeRoles]
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
        [AuthorizeRoles("IsSales")]
        [HttpPost]
        public IHttpActionResult Post(PropertyDtoModel property)
        {
            _propertyFacade.AddProperty(property);
            return Ok(property);
        }

        // PUT: api/PropertyApi/Put
        // I'd keep these roles names in a constant somewhere, they are prone to spellig mistakes.
        [AuthorizeRoles("IsSales", "IsSalesAdmin", "IsSalesDepartmentAdmin")]
        [HttpPut]
        public IHttpActionResult Put(PropertyDtoModel property)
        {
            _propertyFacade.UpdateProperty(property);
            return Ok(property);
        }

        // DELETE: api/PropertyApi/5
        [AuthorizeRoles("IsSalesDepartmentAdmin")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _propertyFacade.DeleteProperty(id);
            return Ok();
        }
    }
}
