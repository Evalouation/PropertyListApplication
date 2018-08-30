using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using System.Collections.Generic;
using PropertyList.Helper;
using PropertyList.BusinessLogic.Constant;
using PropertyList.BusinessLogic.Model;
using PropertyList.BusinessLogic.Providers;

namespace PropertyList.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {      
        private readonly IStaffRoleProvider _roleProvider;
        private readonly IUserAccountResolver _userAccountResolver;
        private readonly string[] allowedroles;

        public AuthorizeRolesAttribute(IStaffRoleProvider roleProvider, IUserAccountResolver userAccountResolver)
        {
            _roleProvider = roleProvider;
            _userAccountResolver = userAccountResolver;
        }

        public AuthorizeRolesAttribute(params string[] roles) : this(new StaffRoleProvider(), new UserAccountResolver())
        {
            this.allowedroles = roles;
        }

        private List<string> GetRoleList()
        {
            return _roleProvider.GetAll().Select(r => r.Name).ToList();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase context)
        {
            UserDtoModel user = _userAccountResolver.GetCurrentUser();
            bool result = false;

            result = IsUserAuthorized(user);
            if (result)
                base.AuthorizeCore(context);
            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult { ViewName = "UnAuthorized" };
        }

        private bool IsUserAuthorized(UserDtoModel user)
        {
            bool isValidUser = false;

            if (allowedroles.Length == 0 || user == null)
                return isValidUser;

            isValidUser = Array.IndexOf(allowedroles, Enum.GetName(typeof(StaffType), user.RoleID)) >= 0;
            return isValidUser;
        }
    }
}