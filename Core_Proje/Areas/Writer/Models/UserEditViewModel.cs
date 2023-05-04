using Microsoft.AspNetCore.Http;

namespace Core_Proje.Areas.Writer.Models
{
    // Profil bilgilerini güncellerken kullanılacak olan model
    public class UserEditViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string PictureUrl { get; set; }
        public IFormFile Picture { get; set; } // Dosyanın wwwroot'a yüklenmesi için gereken tür. Sunucuda çalışıyorsan sunucuya göre ayarlayacaksın.
    }
}
