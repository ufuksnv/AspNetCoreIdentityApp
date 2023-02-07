using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AspNetCoreIdentityApp.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lütfen Parola giriniz !")]
        [Display(Name = "Eski Şifre :")]
        [MinLength(6, ErrorMessage ="Şifreniz en az 6 karakter olabilir")]
        public string? PasswordOld { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lütfen yeni Parolanızı giriniz !")]
        [Display(Name = "Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string? PasswordNew { get; set; }


        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Şifreler uyuşmuyor")]
        [Required(ErrorMessage = "Yeni Şifre tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string? PasswordNewConfirm { get; set; }
    }
}
