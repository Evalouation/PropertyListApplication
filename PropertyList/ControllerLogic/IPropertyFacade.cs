using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.ControllerLogic
{
    public interface IPropertyFacade
    {
        IEnumerable<PropertyDtoModel> GetAllProperties();
        PropertyDtoModel GetById(int id);
    }
}
