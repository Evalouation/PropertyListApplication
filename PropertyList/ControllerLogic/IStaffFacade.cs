using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.ControllerLogic
{
    public interface IStaffFacade
    {
        StaffDtoModel AddStaff(StaffDtoModel staff);
        UserDtoModel ValidateAccount(LoginDtoModel user);
    }
}
