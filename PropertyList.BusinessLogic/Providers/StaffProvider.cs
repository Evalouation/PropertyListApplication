using System;
using System.Collections.Generic;
using System.Linq;
using PropertyList.BusinessLogic.Model;
using PropertyList.Data.UnitOfWork;

namespace PropertyList.BusinessLogic.Providers
{
    public class StaffProvider : IStaffProvider
    {
        private readonly IUnitOfWork _uow;

        #region injection
        public StaffProvider() : this(new UnitOfWork())
        {
        }

        public StaffProvider(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        #endregion

        public IEnumerable<StaffDtoModel> GetAll()
        {
            List<StaffDtoModel> staffs = _uow.GetDB().utStaffs.Select(x => new StaffDtoModel() {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                Role = x.RoleID
            }).ToList();

            return staffs;
        }

        public StaffDtoModel CreateStaff(StaffDtoModel model)
        {
            //todo
            int test = _uow.GetDB().usp_InsertSingleStaff(model.FirstName, model.LastName, model.Email, model.Password,
                model.Role, DateTime.Now.ToLocalTime(), DateTime.Now.ToLocalTime());
            return model;
        }

        public UserDtoModel ValidateStaffAccount(LoginDtoModel account)
        {
            UserDtoModel result = null;
            var user = _uow.GetDB().usp_CheckStaffAccount(account.Email, account.Password).FirstOrDefault();
            if (user != null)
            {
                result = new UserDtoModel()
                {
                     StaffID = user.StaffID,
                     FirstName = user.FirstName,
                     RoleID = user.RoleID
                };

            }
            return result;
        }
    }
}
