using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.ControllerLogic
{
    public interface IStaffFacade
    {
        IEnumerable<StaffDtoModel> GetStaffs();
        StaffDtoModel AddStaff(StaffDtoModel staff);
        UserDtoModel ValidateAccount(LoginDtoModel user);
    }
}
