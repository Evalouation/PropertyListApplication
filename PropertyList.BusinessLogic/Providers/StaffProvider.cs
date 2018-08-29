using System;
using System.Collections.Generic;
using System.Linq;
using PropertyList.BusinessLogic.Model;
using PropertyList.Data.Model;

namespace PropertyList.BusinessLogic.Providers
{
    public class StaffProvider : IStaffProvider
    {
        private PropertyListing_DevEntities db = new PropertyListing_DevEntities(); //To refactor

        public IEnumerable<StaffDtoModel> GetAll()
        {
            List<StaffDtoModel> staffs = db.utStaffs.Select(x => new StaffDtoModel() {
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
            int test = db.usp_InsertSingleStaff(model.FirstName, model.LastName, model.Email, model.Password,
                model.Role, DateTime.Now.ToLocalTime(), DateTime.Now.ToLocalTime());
            return model;
        }

        public UserDtoModel ValidateStaffAccount(LoginDtoModel account)
        {
            UserDtoModel result = null;
            var user = db.usp_CheckStaffAccount(account.Email, account.Password).FirstOrDefault();
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
