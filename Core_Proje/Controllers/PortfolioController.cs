using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PortfolioController : Controller
    {

        PortfolioManager portfolioManager = new PortfolioManager(new EfPortfolioDal());
        public IActionResult Index()
        {
            ViewBag.v1 = "Proje Listesi";
            ViewBag.v2 = "Projeler";
            ViewBag.v3 = "Proje Listesi";
            var values = portfolioManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddPortfolio()
        {
            ViewBag.v1 = "Proje Ekle";
            ViewBag.v2 = "Projeler";
            ViewBag.v3 = "Proje Ekle";
            return View();
        }

        [HttpPost]
        public IActionResult AddPortfolio(Portfolio portfolio)
        {
            // Validasyonlara erişmek için o classtan bir nesne türettik.
            PortfolioValidator validations = new PortfolioValidator();

            // Türetilen nesne üzerinden, parametreden gönderilen değerin(portfolio) geçerliliğini kontrol et. ve bunun sonucunu resltsta tut.
            ValidationResult results = validations.Validate(portfolio);

            // Eğer validasyon geçerliyse
            if (results.IsValid)
            {
                portfolioManager.TAdd(portfolio);

                return RedirectToAction("Index");
            }
            else
            { 
                // validasyon geçerli değilse  
                foreach(var item in results.Errors)
                {
                    // Property name'e göre,ilgili property'nin hata mesajını yazdır.
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditPortfolio(int id)
        {
            ViewBag.v1 = "Proje Güncelle";
            ViewBag.v2 = "Projeler";
            ViewBag.v3 = "Proje Güncelle";

            var values = portfolioManager.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult EditPortfolio(Portfolio portfolio)
        {
            portfolioManager.TUpdate(portfolio);
            return RedirectToAction("Index");
        }

        public IActionResult DeletePortfolio(int id)
        {
            var values = portfolioManager.TGetByID(id);
            portfolioManager.TDelete(values);
            return RedirectToAction("Index");
        }
    }
}
