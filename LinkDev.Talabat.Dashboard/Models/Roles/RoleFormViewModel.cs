using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.Models.Roles
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
