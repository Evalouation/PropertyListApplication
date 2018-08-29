using System;
using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;
using PropertyList.BusinessLogic.Providers;

namespace PropertyList.ControllerLogic
{
    public class StaffFacade : IStaffFacade
    {
        private readonly IStaffProvider _staffProvider;

        public StaffFacade() : this(new StaffProvider())
        {
        }

        public StaffFacade(IStaffProvider staffProvider)
        {
            _staffProvider = staffProvider;
        }

        public IEnumerable<StaffDtoModel> GetStaffs()
        {
            try
            {
                return _staffProvider.GetAll();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public StaffDtoModel AddStaff(StaffDtoModel staff)
        {
            try
            {
                return _staffProvider.CreateStaff(staff);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public UserDtoModel ValidateAccount(LoginDtoModel user)
        {
            try
            {
                return _staffProvider.ValidateStaffAccount(user);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
    }
}