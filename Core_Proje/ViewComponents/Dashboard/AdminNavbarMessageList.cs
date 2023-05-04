using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DataAccessLayer.Concrete;
using Core_Proje.Models;

namespace Core_Proje.ViewComponents.Dashboard
{
    public class AdminNavbarMessageList : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            Context context = new Context();
            var filter = "admin@gmail.com";

            var list = (from x in context.Users
                        join y in context.WriterMessages
                        on x.Email equals y.Sender
                        where y.Receiver == filter
                        select new ImageModel
                        {
                            ImageUrl = x.ImageUrl,
                            Date = y.Date,
                            SenderName = y.SenderName,
                            MessageContent = y.MessageContent,
                            Id = y.WriterId
                        }).OrderByDescending(x => x.Id).Take(3).ToList();
            return View(list);
        }
    }

    //WriterMessageManager writerMessageManager = new WriterMessageManager(new EfWriterMessageDal());
    //public IViewComponentResult Invoke()
    //{
    //    string adminMail = "admin@gmail.com";

    //    // AdminMail değişkeniyle verilen mailin alıcısı oldugu mesajları, wirterID'ye göre desc şekilde( z'den a'ya) sırala, ilk 3'ünü al. YBu şekilde gelen son 3 mesajı listeleyeceğiz. 
    //    var values = writerMessageManager.GetListReceiverMessage(adminMail).OrderByDescending(x=>x.WriterId).Take(3).ToList();

    //    Context c= new Context();

    //    // WriterMessage tablosundan receiver'ı adminMail olan veriler içinden sender'ı senderMail değişkenine ata.
    //    var senderMail=c.WriterMessages.Where(x=>x.Receiver==adminMail).Select(z=>z.Sender).FirstOrDefault();

    //    // User tablosundan emaili sendermail'e eşit olan verilerin imageUrl'sini image değişkenine ata.
    //    var image =c.Users.Where(x=>x.Email==senderMail).Select(y=>y.ImageUrl).FirstOrDefault();

    //    ViewBag.WriterImage = image;
    //    return View(values);
    //}
}

