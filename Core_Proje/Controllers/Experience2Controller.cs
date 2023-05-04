using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core_Proje.Controllers
{
    public class Experience2Controller : Controller
    {
        ExperienceManager experienceManager=new ExperienceManager(new EfExperienceDal());
        public IActionResult Index()
        {
            // Gönderilen değerleri json türüne convert et. Listeleme işlemlerinde SerializeObject kullanılacak, birşeyler gönderirken DeserializeObject kullanılacak.

            return View();
        }
        public IActionResult ListExperience()
        {
            var values = JsonConvert.SerializeObject(experienceManager.TGetList());
            return Json(values);
        }

        [HttpPost]
        public IActionResult AddExperience(Experience p)
        {
            experienceManager.TAdd(p);
            var values = JsonConvert.SerializeObject(p);
            return Json(values);
        }
        public IActionResult GetByID(int ID)
        {
            var experience=experienceManager.TGetByID(ID);
            var values= JsonConvert.SerializeObject(experience);
            return Json(values);
        }
        public IActionResult DeleteExperience(int ID)
        {
            var experience = experienceManager.TGetByID(ID);
            experienceManager.TDelete(experience);
            return NoContent();
        }
        [HttpPost]
        public IActionResult UpdateExperience(int id, string name, string date)
        {
            var findvalue = experienceManager.TGetByID(id);

            if (findvalue != null)
            {
                findvalue.Name = name;
                findvalue.Date = date;
                experienceManager.TUpdate(findvalue);
                var val = JsonConvert.SerializeObject(findvalue);

                return Json(val);
            }
            return NoContent();
        }
    }
}
