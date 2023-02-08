using AspNetCoreIdentityApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AspNetCoreIdentityApp.ViewModels
{
    public class UserEditViewModel
    {

        [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz !")]
        [Display(Name = "Kullanıcı Adı :")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Lütfen Mail giriniz !")]
        [Display(Name = "Email :")]
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Lütfen Telefon Numarası giriniz !")]
        [Display(Name = "Telefon Numarası :")]
        public string? Phone { get; set; }

        [Display(Name ="Doğum tarihi :")]
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Şehir :")]
        public string? City { get; set; }

        [Display(Name ="Profil resmi :")]
        public IFormFile? Picture { get; set; }

        [Display(Name ="Cinsiyet")]
        public Gender? Gender { get; set; }

    }
}
