using PropertyList.BusinessLogic.Constant;
using System.ComponentModel.DataAnnotations;

namespace PropertyList.Models
{
    public class StaffViewModel
    {
        [Key]
        public int StaffID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        public StaffType Role { get; set; }
    }
}