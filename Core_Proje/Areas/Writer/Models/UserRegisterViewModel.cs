using System.ComponentModel.DataAnnotations;

namespace Core_Proje.Areas.Writer.Models
{
    // Sisteme üye olurken kullanılacak olan model
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage ="Lütfen  Adınızı Girin.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Lütfen Soyadınızı Girin.")]
        public string Surname { get; set; }
        [Required(ErrorMessage ="Lütfen Kullanıcı Adınızı Girin.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Girin.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Girin.")]
        [Compare("Password",ErrorMessage ="Girilen Şifreler Aynı Değil.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Lütfen Mail Adresinizi Girin.")]
        public string Mail { get; set; } 

        [Required(ErrorMessage = "Lütfen Resme Ait Url'yi Girin.")]
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }

    }
}
