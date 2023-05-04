using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "İstatistikler Sayfası";
            ViewBag.v2 = "İstatistikler";
            ViewBag.v3 = "İstatistikler Sayfası";
            return View();
        }
    }
}
