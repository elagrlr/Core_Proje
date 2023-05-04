using BusinessLayer.Concrete;
using Core_Proje.Areas.Writer.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core_Proje.Controllers
{
    public class WriterUserController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        private readonly UserManager<WriterUser> _userManager;

        public WriterUserController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = "Kullanıcı Listele";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Kullanıcı Listele";
            var values = writerManager.TGetList();

            return View(values);
        }

        [HttpGet]
        public IActionResult AddWriter()
        {
            ViewBag.v1 = "Kullanıcı Ekle";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Kullanıcı Ekle";

            return View(new UserRegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddWriter(UserRegisterViewModel p) // Aşağıda async metot kullandığımız için action'ın da async olması lazım. Bu yüzden async Task< IActionResult> şeklinde tanımlandı.
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
                    return RedirectToAction("Index");
                }
                else // İşlem başarılı değilse
                {
                    foreach (var item in resulst.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
        string id1 = "";
        [HttpGet]
        public async Task<IActionResult> EditWriter(string id)
        {
            id1 = id;
            ViewBag.v1 = "Kullanıcı Güncelle";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Kullanıcı Güncelle";

            var values = await _userManager.FindByIdAsync(id);
            UserEditViewModel model = new UserEditViewModel();
            model.Name = values.Name;
            model.Surname = values.Surname;
            model.PictureUrl = values.ImageUrl;

            return View(model );
        }

        [HttpPost]
        public async Task<IActionResult> EditWriter(UserEditViewModel p)
        {
            var values = await _userManager.FindByIdAsync(id1);
            values.Name=p.Name;
            values.Surname=p.Surname;  
            values.PasswordHash= _userManager.PasswordHasher.HashPassword(values, p.Password);
            var result = await _userManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult DeleteWriter(int id)
        {
            var values = writerManager.TGetByID(id);
            writerManager.TDelete(values);
            return RedirectToAction("Index");
        }
    }
}
