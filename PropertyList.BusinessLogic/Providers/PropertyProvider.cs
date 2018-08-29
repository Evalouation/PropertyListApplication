using System.Linq;
using System.Collections.Generic;
using PropertyList.BusinessLogic.Model;
using PropertyList.Data.Model;

namespace PropertyList.BusinessLogic.Providers
{
    public class PropertyProvider : IPropertyProvider
    {
        private PropertyListing_DevEntities db = new PropertyListing_DevEntities(); //To refactor

        public IEnumerable<PropertyDtoModel> GetAll()
        {
            List<PropertyDtoModel> result  = db.usp_SelectAllProperties().Select(p => new PropertyDtoModel()
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
            var p = db.usp_GetPropertyById(id).FirstOrDefault();
            return  p != null ? new PropertyDtoModel {
                Location = p.Location,
                Bedroom = p.Bedroom.HasValue ? p.Bedroom.Value : 0,
                Bathroom = p.Bathroom.HasValue ? p.Bathroom.Value : 0,
                ConfidentialNotes = p.ConfidentialNotes,
                Status = p.Status
            } : null;
           
        }
    }
}
