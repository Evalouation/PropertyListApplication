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
    }
}