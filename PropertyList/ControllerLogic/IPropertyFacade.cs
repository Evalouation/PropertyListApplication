using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.ControllerLogic
{
    public interface IPropertyFacade
    {
        IEnumerable<PropertyDtoModel> GetAllProperties();
        PropertyDtoModel GetById(int id);
        PropertyDtoModel AddProperty(PropertyDtoModel property);
        PropertyDtoModel UpdateProperty(PropertyDtoModel property);
        bool DeleteProperty(int id);
    }
}
