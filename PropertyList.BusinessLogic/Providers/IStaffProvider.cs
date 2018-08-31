using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.BusinessLogic.Providers
{
    public interface IStaffProvider
    {
        UserDtoModel ValidateStaffAccount(LoginDtoModel account);
        StaffDtoModel GetByEmail(string email);
        StaffDtoModel CreateStaff(StaffDtoModel model);
    }
}
