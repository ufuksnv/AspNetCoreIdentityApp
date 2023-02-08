using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage ="Role ismi boş bırakılamaz")]
        [Display(Name ="Role ismi :")]
        public string? Name { get; set; }
    }
}
