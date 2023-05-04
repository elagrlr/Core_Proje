using Core_Proje.Areas.Writer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core_Proje.Areas.Writer.Controllers
{
    [AllowAnonymous]

    [Area("Writer")]

    // Yönlendirme işlemi. Writer'ın içindeki controller'daki action'ı bul ve çağır. Bu url'yi takip ederek işlem yap. RedirectToAction derken artık yalnızca view'in adını vermemiz yeterli olacak.
    [Route("Writer/[controller]/[action]")]
    public class RegisterController : Controller
    {

        private readonly UserManager<WriterUser> _userManager;

        public RegisterController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserRegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel p) // Aşağıda async metot kullandığımız için action'ın da async olması lazım. Bu yüzden async Task< IActionResult> şeklinde tanımlandı.
        {
            if (ModelState.IsValid)
            {
                WriterUser writerUser = new WriterUser()
                {
                    Name = p.Name,
                    Surname = p.Surname,
                    UserName = p.UserName,
                    Email = p.Mail,
                    ImageUrl = p.ImageUrl
                };

                // Eğer model state geçerliyse, aşağıdaki async komut çalışacak. Create diyoruz çünkü hesap oluşturuyoruz. Yukarda tanımlanan _userManager ile oluşturuyoruz bu hesabı ve şifreyi de burada tanımlıyoruz.


                if (p.ConfirmPassword == p.Password)// Eğer işlem başarılıysa, dışasrıdan girilen password ve confirm password eşitse
                {
                    var resulst = await _userManager.CreateAsync(writerUser, p.Password);
                    if (resulst.Succeeded)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else // İşlem başarılı değilse
                    {
                        foreach (var item in resulst.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }

            }
            return View();
        }
    }
}
