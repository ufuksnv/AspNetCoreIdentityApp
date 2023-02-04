using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {

        }


        [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz !")]
        [Display(Name ="Kullanıcı Adı :")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Lütfen Mail giriniz !")]
        [Display(Name = "Email :")]
        [EmailAddress(ErrorMessage ="Email formatı yanlıştır.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Lütfen Telefon Numarası giriniz !")]
        [Display(Name = "Telefon Numarası :")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Lütfen Parola giriniz !")]
        [Display(Name = "Şifre :")]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        public string? PasswordConfirm { get; set; }
    }
}
