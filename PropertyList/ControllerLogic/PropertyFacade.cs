using System;
using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;
using PropertyList.BusinessLogic.Providers;

namespace PropertyList.ControllerLogic
{
    public class PropertyFacade : IPropertyFacade
    {
        private readonly IPropertyProvider _PropertyProvider;

        public PropertyFacade() : this(new PropertyProvider())
        {
        }

        public PropertyFacade(IPropertyProvider PropertyProvider)
        {
            _PropertyProvider = PropertyProvider;
        }

        public IEnumerable<PropertyDtoModel> GetAllProperties()
        {
            return _PropertyProvider.GetAll();   
        }

        public PropertyDtoModel GetById(int id)
        {
            return _PropertyProvider.GetById(id);
        }

        public PropertyDtoModel AddProperty(PropertyDtoModel property)
        {
            return _PropertyProvider.CreateProperty(property);
        }

        public PropertyDtoModel UpdateProperty(PropertyDtoModel property)
        {
            return _PropertyProvider.UpdateProperty(property);
        }

        public bool DeleteProperty(int id)
        {
            return _PropertyProvider.DeleteProperty(id);
        }
    }
}