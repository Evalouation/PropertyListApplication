using System.Web.Http;
using PropertyList.BusinessLogic.Model;
using PropertyList.ControllerLogic;

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
                return Ok(user);
            
            return Unauthorized();
        }
    }
}
