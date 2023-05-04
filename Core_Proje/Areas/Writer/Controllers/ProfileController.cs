using Core_Proje.Areas.Writer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core_Proje.Areas.Writer.Controllers
{
    // TODO
    // Şifre ve resim güncelleme farklı sayfalarda olabilir

[Area("Writer")]
    public class ProfileController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public ProfileController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Sisteme giriş yapan kullanıcının kullanıcı adını getir.
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel model = new UserEditViewModel();
            model.Name = values.Name;
            model.Surname = values.Surname;
            model.PictureUrl = values.ImageUrl;
           

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Paramerterden gelen picrute null değilse, resim ataması yap.
            if (p.Picture != null)
            {
                // Aktif yolu(proje yolunu) al
                var resource = Directory.GetCurrentDirectory();

                // Uzantısına p'nin picture'ının file name'ini(.jpeg .png gibi) al
                var extension = Path.GetExtension(p.Picture.FileName);

                // Resmin ismi için benzersiz bi isim oluşturulur. Bu benzersiz isim için guid kullanılır.Image name guid+extension'dan(a07b344e-d0e1-41f8-91de-897d24beb79f.jpg gibi) oluşur.
                var imagename = Guid.NewGuid() + extension;

                // Resmin kaydedileceği yer= kaynaktan gelen değer+ wwwroot altındaki userimage kalsörüne + imagename'den gelen değer ile kaydet.
                var savelocation = resource + "/wwwroot/userimage/" + imagename;

                // Akış(stream)= Kaydedilecek lokasyon ve Filemode verilerek oluşturulur. Burda FileStream bir path ve mode istiyor, path= savelocation ve mode= FileMode.Create olarak vereceğiz. Böylece resim dosyası oluşturulmuş olacak.
                var stream = new FileStream(savelocation, FileMode.Create);

                // p'nin picture'ına stream'den gelen akış değerini kopyala.
                await p.Picture.CopyToAsync(stream);

                // User'ın Imagerurl'sine de imagename'den gelen değeri veriyoruz.
                user.ImageUrl = imagename;
            }
            //Güncelleme işlemi yaptığımız için atamaları yap.
            user.Name = p.Name;
            user.Surname = p.Surname;

            // Şifreyi Güncelleme
            // Şifreler hashlenmiş sekilde saklandığı için paswordhasher adlı hazır metotla şifre güncelleme işlemini yapıyoruz. Bu metot aşağıdaki gibi user ve password'u parametre olarak almaktadır.
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, p.Password);

            //user'dan gelen değerlere göre güncelleme işlemini gerçekleştir.
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Login");
            }


            return View();
        }
    }
}
