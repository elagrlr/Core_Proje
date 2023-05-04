using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core_Proje.Areas.Writer.Models
{
    // Sisteme giriş yaparken kullanılacak olan model
    public class UserLoginViewModel
    {
        [Display(Name="Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı Adınızı Girin...")]
        public string UserName { get; set; }
        [Display(Name = "Şifre ")]
        [Required(ErrorMessage = "Şifrenizi Girin...")]
        public string Password { get; set; }
    }
}
