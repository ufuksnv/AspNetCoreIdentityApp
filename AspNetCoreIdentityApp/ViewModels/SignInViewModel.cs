using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AspNetCoreIdentityApp.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
        }

        [Required(ErrorMessage = "Lütfen Mail giriniz !")]
        [Display(Name = "Email :")]
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Lütfen Parola giriniz !")]
        [Display(Name = "Şifre :")]        
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
