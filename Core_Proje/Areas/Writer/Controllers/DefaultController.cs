using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Areas.Writer.Controllers
{
    /// Writer adlı area ile çalıştığını bu şekilde belirtmemiz gerekli.
    [Area ("Writer")]
     
     // Bu sayfayı görebilmen için, once authantice olmalısın diyor. Startup.cs'ye de app.UseAuthentication(); metodu eklendi bunun için.
    
    public class DefaultController : Controller
    {
        AnnouncementManager announcementManager = new AnnouncementManager(new EfAnnouncementDal ());
        public IActionResult Index()
        {
             var values=announcementManager.TGetList();
            return View(values);
        }
        [Route("Writer/[controller]/[action]/{id}")]
        public IActionResult AnnouncementDetails(int id)
        {
            Announcement announcement = announcementManager.TGetByID(id);
            return View(announcement);
        }
    }
}
