using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core_Proje.ViewComponents.Dashboard
{
    public class Last5Projects : ViewComponent
    {
       SkillManager skillManager=new SkillManager(new EfSkillDal());
        
        public IViewComponentResult Invoke()
        
        {
           var values=skillManager.TGetList().OrderByDescending(x=>x.Value).Take(6).ToList(); 

            return View(values);
        }
    } 
}
