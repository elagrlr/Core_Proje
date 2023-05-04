using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core_Proje.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public DashboardController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var values=await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.v = values.Name + " " + values.Surname;

            // Weather api
            // Api key'i tanımlıyoruz.
            string api = "";

            // Bağlantı değerini tanımlıyoruz. appid değeri yukarıdaki api stringinden gelecek.
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=kayseri&mode=xml&lang=tr&units=metric&appid="+api;

            // Connection'dan gelen değeri yükle(load)
            XDocument document= XDocument.Load(connection);

            //v5 viewbag'ine de document'den gelen değerleri desc şekilde sırala. Hangi değeri sırala temperature tagli değeri sırala. Burdan gelen 0.indexli değeri getir. Temperature içinde birçok değer olduğundna hangi değerin/attribute'ün getirilmesi gerektiğini Attribute("value") şeklinde belirt. Bunun da value'sunu v5'e at.
            ViewBag.v5 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            // Statistics component yapmaya calış
            Context c=new Context();
            ViewBag.v1 = c.WriterMessages.Where(x=>x.Receiver==values.Email).Count();
            ViewBag.v2 = c.Announcements.Count();
            ViewBag.v3 = c.Users.Count();
            ViewBag.v4 = c.WriterMessages.Where(x => x.Sender == values.Email).Count();

            return View();
        }
    }
}
/*
 https://api.openweathermap.org/data/2.5/weather?q=kayseri&mode=xml&lang=tr&units=metric&appid=32aec93e4127989052ea36ff1249d0f3
*/