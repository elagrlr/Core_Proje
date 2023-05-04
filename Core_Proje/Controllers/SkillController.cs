using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SkillController : Controller
    {

        SkillManager skillManager = new SkillManager(new EfSkillDal());
        public IActionResult Index()
        {
            ViewBag.v1 = "Yetenek Listesi";
            ViewBag.v2 = "Yetenekler";
            ViewBag.v3 = "Yetenek Listesi";

            var values = skillManager.TGetList();

            return View(values);
        }
        [HttpGet]
        public IActionResult AddSkill()
        {
            ViewBag.v1 = "Yetenek Ekleme";
            ViewBag.v2 = "Yetenekler";
            ViewBag.v3 = "Yetenek Ekleme";

            return View();
        }
        [HttpPost]
        public IActionResult AddSkill(Skill skill)
        {
            // Skille ekleme yap
            skillManager.TAdd(skill);

            // İşlemi yaptıktan sonra beni index'e yönlendir.
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSkill(int id)
        {
            // Önce silinecek ögeyi bul
            var silinecekDeger = skillManager.TGetByID(id);

            // Silme işlemini yap
            skillManager.TDelete(silinecekDeger);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditSkill(int id)
        {
            ViewBag.v1 = "Yetenek Güncelleme";
            ViewBag.v2 = "Yetenekler";
            ViewBag.v3 = "Yetenek Güncelleme";

            var values = skillManager.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult EditSkill(Skill skill)
        {
            skillManager.TUpdate(skill);

            return RedirectToAction("Index");
        }



    }

}
