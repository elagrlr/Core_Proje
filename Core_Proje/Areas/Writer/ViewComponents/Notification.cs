using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace Core_Proje.Areas.Writer.ViewComponents
{
    public class Notification:ViewComponent
    {
        AnnouncementManager announcementManager=new AnnouncementManager(new EfAnnouncementDal());
        public IViewComponentResult Invoke()
        {
            // İlk 5 bildiiemi listele
            var values = announcementManager.TGetList().Take(5).ToList();
            return View(values);
        }
    }
}
