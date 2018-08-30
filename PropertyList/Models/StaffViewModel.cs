using PropertyList.BusinessLogic.Constant;
using System.ComponentModel.DataAnnotations;

namespace PropertyList.Models
{
    public class StaffViewModel
    {
        [Key]
        public int StaffID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public StaffType Role { get; set; }
    }
}