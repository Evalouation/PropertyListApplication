using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.BusinessLogic.Providers
{
    public interface IStaffProvider
    {
        UserDtoModel ValidateStaffAccount(LoginDtoModel account);

        IEnumerable<StaffDtoModel> GetAll();

        //ArticleDetailModel GetArticleDetail(int id);
        StaffDtoModel CreateStaff(StaffDtoModel model);
    }
}
