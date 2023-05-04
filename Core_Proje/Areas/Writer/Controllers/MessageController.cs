using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Proje.Areas.Writer.Controllers
{
    [Area("Writer")]

    // Yönlendirme işlemi. Writer'ın içindeki controller'daki action'ı bul ve çağır. Bu url'yi takip ederek işlem yap. RedirectToAction derken artık yalnızca view'in adını vermemiz yeterli olacak.(ÖR:SendMessage'daki return ifadesi)
    [Route("Writer/Message")]
    public class MessageController : Controller
    {
        WriterMessageManager _writerMessageManager = new WriterMessageManager(new EfWriterMessageDal());
        private readonly UserManager<WriterUser> _userManager;

        public MessageController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("")]
        [Route("ReceiverMessage")]
        public async Task<IActionResult> ReceiverMessage(string p)
        {
            // Sisteme giriş yapan kullanıcının kullanıcı adını getir.
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            // Session'dan gelen p'ye value'nun emailini ata
            p = values.Email;

            // p'deki emaile göre filtreleme işlemi yap ve sonucu messasgeListe ata
            var messageList = _writerMessageManager.GetListReceiverMessage(p);

            return View(messageList);
        }

        [Route("")]
        [Route("SenderMessage")]
        public async Task<IActionResult> SenderMessage(string p)
        {
            // Sisteme giriş yapan kullanıcının kullanıcı adını getir.
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            // Session'dan gelen p'ye value'nun emailini ata
            p = values.Email;

            // p'deki emaile göre filtreleme işlemi yap ve sonucu messasgeListe ata
            var messageList = _writerMessageManager.GetListSenderMessage(p);

            return View(messageList);
        }

        [Route("MessageDetails/{id}")]
        public IActionResult MessageDetails(int id)
        {
            WriterMessage writerMessage = _writerMessageManager.TGetByID(id);
            return View(writerMessage);
        }

        [Route("ReceiverMessageDetails/{id}")]
        public IActionResult ReceiverMessageDetails(int id)
        {
            WriterMessage writerMessage = _writerMessageManager.TGetByID(id);
            return View(writerMessage);
        }
        [HttpGet]
        [Route("")]
        [Route("SendMessage")]
        public IActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        [Route("")]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(WriterMessage p)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string mail = values.Email;
            string name = values.Name + " " + values.Surname;

            p.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.Sender = mail;
            p.SenderName = name;
            Context c=new Context();

            // Context'deki Users tablosundan,Emaili p'nin receiver'ına eşit olan verinin; adı ve soyadını seç. Bu seçme işlemiyle de first or default yani yakaldaığın ilk değeri getir.
            var userNameSurname = c.Users.Where(x=>x.Email==p.Receiver).Select(y=>y.Name+ " "+y.Surname).FirstOrDefault();

            // Alıcı ad soyadına userNameSurname'deki değeri ata.
            p.ReceiverName=userNameSurname;
            _writerMessageManager.TAdd(p);
            return RedirectToAction("SenderMessage");
        }
        public IActionResult ViewDataDenemesi()
        {
            WriterMessage message = new WriterMessage()
            {
                WriterId = 1,
                Date = DateTime.Now,
                MessageContent = "viewdata deneme mesajı",
                Receiver = "viewdata@gmail.com",
                ReceiverName = "View Data",
                Sender = "denemetest@gmail.com",
                SenderName = "test adı test soyadı",
                Subject = "bu bir viewdata ile veri gönderme örneğidir."
            };
            ViewData["writer"] = message;

            ViewData["writer2"] = "deneme";

            var values = _writerMessageManager.TGetList();
            ViewData["writer3"] = values;

            ViewBag.v1 = _writerMessageManager.TGetList();
            ViewBag.v2 = "foreach ile ViewBag   denemesi";

            TempData["veriadi"] = "TempData denemesi";
            TempData["veri"] = _writerMessageManager.TGetList();
            return View();
        }
    }
}
