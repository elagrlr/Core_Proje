using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core_Proje.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult NavbarPartial() {
            return PartialView();
        }

        MessageManager messageManager = new MessageManager(new EfMessageDal());

        [HttpGet]
        public PartialViewResult SendMessage()
        { 
            return PartialView( );
        }
        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            message.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            message.Status = true;
            messageManager.TAdd(message);

            return RedirectToAction("Index", "Default");
        }
    }
}
