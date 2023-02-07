using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AspNetCoreIdentityApp.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Lütfen Parola giriniz !")]
        [Display(Name = "Şifre :")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        public string? PasswordConfirm { get; set; }
    }
}
