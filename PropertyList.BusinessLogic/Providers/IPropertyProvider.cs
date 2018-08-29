using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;

namespace PropertyList.BusinessLogic.Providers
{
    public interface IPropertyProvider
    {
        IEnumerable<PropertyDtoModel> GetAll();
        PropertyDtoModel GetById(int id);
        PropertyDtoModel CreateProperty(PropertyDtoModel model);
        PropertyDtoModel UpdateProperty(PropertyDtoModel model);
    }
}
