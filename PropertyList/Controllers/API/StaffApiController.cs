using System;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Newtonsoft.Json;
using PropertyList.BusinessLogic.Model;
using PropertyList.ControllerLogic;
using PropertyList.Models;

namespace PropertyList.Controllers.API
{
    public class StaffApiController : ApiController
    {  
        private readonly IStaffFacade _staffFacade;

        public StaffApiController(): this( new StaffFacade())
        {
        }

        public StaffApiController(IStaffFacade staffFacade)
        {
            _staffFacade = staffFacade;
        }

        // GET: api/Staff/5
        public IHttpActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST: api/Staff/Post
        [HttpPost]
        public IHttpActionResult Post(StaffDtoModel staff)
        {
            _staffFacade.AddStaff(staff);
            return Ok(staff);
        }

        // POST: api/Staff/ValidateAccount
        [HttpPost]
        public IHttpActionResult ValidateAccount(LoginDtoModel account)
        {
            UserDtoModel user = _staffFacade.ValidateAccount(account);

            if (user != null)
            { 
                
                return Ok(user);
            }
            else
                return Unauthorized();
        }

        //// PUT: api/Staff/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Staff/5
        //public void Delete(int id)
        //{
        //}
    }
}
