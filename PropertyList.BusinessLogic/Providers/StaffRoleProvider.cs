using System.Collections.Generic;
using System.Linq;
using PropertyList.BusinessLogic.Model;
using PropertyList.Data.UnitOfWork;

namespace PropertyList.BusinessLogic.Providers
{
    public class StaffRoleProvider : IStaffRoleProvider
    {
        private readonly IUnitOfWork _uow;

        #region injection
        public StaffRoleProvider() : this(new UnitOfWork())
        {
        }

        public StaffRoleProvider(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        #endregion

        //public bool IsUserInRole(string username, string roleName)
        //{

        //}

        public IEnumerable<RoleDtoModel> GetAll()
        {
            return _uow.GetDB().usp_GetAllRoles().Select(r => new RoleDtoModel()
            {
                RoleID = r.RoleID,
                Name = r.Name
            }).ToList();
        }
    }
}
