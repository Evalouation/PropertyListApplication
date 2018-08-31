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

        public StaffDtoModel CreateStaff(StaffDtoModel model)
        {
            _uow.GetDB().usp_InsertSingleStaff(model.FirstName, model.LastName, model.Email, model.Password,
                model.Role, DateTime.Now.ToLocalTime(), DateTime.Now.ToLocalTime());
            return model;
        }

        public UserDtoModel ValidateStaffAccount(LoginDtoModel account)
        {
            UserDtoModel result = null;
            var queryResult = _uow.GetDB().usp_CheckStaffAccount(account.Email, account.Password);
            if (queryResult != null)
            {
                var user = queryResult.FirstOrDefault();
                result = new UserDtoModel()
                {
                    StaffID = user.StaffID,
                    FirstName = user.FirstName,
                    RoleID = user.RoleID
                };
            }
            return result;
        }

        public StaffDtoModel GetByEmail(string email)
        {
            var queryResult = _uow.GetDB().usp_GetStaffByEmail(email);
            if (queryResult != null)
            {
                var s = queryResult.FirstOrDefault();
                return new StaffDtoModel
                {
                    StaffID = s.StaffID,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    Role = s.RoleID
                };
            }
            return null;
        }
    }
}
