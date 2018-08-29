using System.ComponentModel.DataAnnotations;
using PropertyList.BusinessLogic.Constant;

namespace PropertyList.Models
{
    public class PropertyViewModel
    {
        [Key]
        public int PropertyID { get; set; }
        public string Location { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public string ConfidentialNotes { get; set; }
        public PropertyStatus Status { get; set; }
        //public bool IsDeleted { get; set; }
    }
}