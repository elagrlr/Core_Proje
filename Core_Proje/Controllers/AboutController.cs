﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.v1 = "Hakkımda Düzenleme";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Düzenleme";

            var values =aboutManager.TGetByID(1);
            return View(values);
        }

        [HttpPost]
        public IActionResult Index(About about)
        {
           aboutManager.TUpdate(about);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}