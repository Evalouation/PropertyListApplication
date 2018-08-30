using PropertyList.BusinessLogic.Model;

namespace PropertyList.Helper
{
    public interface IUserAccountResolver
    {
        UserDtoModel GetCurrentUser();
        int GetCurrentUserRole();

    }
}
