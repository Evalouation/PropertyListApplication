using System;
using System.Linq;
using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;
using PropertyList.Data.UnitOfWork;

namespace PropertyList.BusinessLogic.Providers
{
    public class PropertyProvider : IPropertyProvider
    {
        private readonly IUnitOfWork _uow;

        #region injection
        public PropertyProvider() : this(new UnitOfWork())
        {
        }

        public PropertyProvider(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        #endregion

        public IEnumerable<PropertyDtoModel> GetAll()
        {
            List<PropertyDtoModel> result  = _uow.GetDB().usp_GetAllProperties().Select(p => new PropertyDtoModel()
            {
                PropertyID = p.PropertyID,
                Location = p.Location,
                Bedroom = p.Bedroom.HasValue ? p.Bedroom.Value : 0,
                Bathroom = p.Bathroom.HasValue ? p.Bathroom.Value : 0,
                ConfidentialNotes = p.ConfidentialNotes,
                Status = p.Status
            }).ToList();

            return result;
        }

        public PropertyDtoModel GetById(int id)
        {
            var p = _uow.GetDB().usp_GetPropertyById(id).FirstOrDefault();
            return  p != null ? new PropertyDtoModel {
                PropertyID = id,
                Location = p.Location,
                Bedroom = p.Bedroom.HasValue ? p.Bedroom.Value : 0,
                Bathroom = p.Bathroom.HasValue ? p.Bathroom.Value : 0,
                ConfidentialNotes = p.ConfidentialNotes,
                Status = p.Status
            } : null;
           
        }

        public PropertyDtoModel CreateProperty(PropertyDtoModel model)
        {
            _uow.GetDB().usp_InsertSingleProperty(model.Location, model.Bedroom, model.Bathroom, model.ConfidentialNotes,
                model.Status, false, DateTime.Now.ToLocalTime(), DateTime.Now.ToLocalTime());
            return model;
        }

        public PropertyDtoModel UpdateProperty(PropertyDtoModel model)
        {
            _uow.GetDB().usp_UpdateProperty(model.PropertyID, model.Location, model.Bedroom, model.Bathroom, model.ConfidentialNotes,
                model.Status, false, DateTime.Now.ToLocalTime());
            return model;
        }

        public bool DeleteProperty(int id)
        {
            var p = _uow.GetDB().usp_GetPropertyById(id).FirstOrDefault();
            if (p == null)
                return false;
            _uow.GetDB().usp_DeleteProperty(id);
            return true;
        }
    }
}
