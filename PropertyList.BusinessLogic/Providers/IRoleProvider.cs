using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.BusinessLogic.Providers
{
    public interface IStaffRoleProvider
    {
        IEnumerable<RoleDtoModel> GetAll();
    }
}
