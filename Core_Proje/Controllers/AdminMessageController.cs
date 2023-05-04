using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminMessageController : Controller
    {
        WriterMessageManager writerMessageManager = new WriterMessageManager(new EfWriterMessageDal());
       
       // Admine gönderilen mesajlar için default bir meail adresi oluşturduk.
            string adminMail = "admin@gmail.com";
        public IActionResult ReceiverMessageList()
        {
            TempData["DeleteUrl"] = "ReceiverMessageList";
            // Alıcısı oldugumuz mesajları getir.
            var values = writerMessageManager.GetListReceiverMessage(adminMail);
            return View(values);
        }
        public IActionResult SenderMessageList()
        {
            TempData["DeleteUrl"] = "SenderMessageList";
            // Göndericisi oldugumuz mesajları getir.
            var values = writerMessageManager.GetListSenderMessage(adminMail);
            return View(values);
        }
        public IActionResult AdminMessageDetails(int id)
        {
            var values = writerMessageManager.TGetByID(id);
            return View(values);
        }
        public IActionResult DeleteAdminMessage(int id)
        {
            string viewUrl = TempData["DeleteUrl"].ToString();
            var values = writerMessageManager.TGetByID(id);
            writerMessageManager.TDelete(values);
            return RedirectToAction(viewUrl);
        }
        [HttpGet]
        public IActionResult AdminMessageSend()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult AdminMessageSend(WriterMessage w)
        {
            w.Sender = adminMail;
            w.SenderName = "Admin";
            w.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            Context c = new Context(); 
            var userNameSurname = c.Users.Where(x => x.Email == w.Receiver).Select(y => y.Name + " " + y.Surname).FirstOrDefault();
            w.ReceiverName=userNameSurname;
            writerMessageManager.TAdd(w);
            return RedirectToAction("SenderMessageList");

        }
    }
}
