using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.ViewComponents.Service
{
    public class ServiceList : ViewComponent
    {
        ServiceManager serviceManager = new ServiceManager(new EfServiceDal() );
        public IViewComponentResult Invoke()
        {
            var values = serviceManager.TGetList();
            return View(values);
        }
    }
}



//View component klasörüne yeni bir klasör ekle. Örneğin services bölümü üzerinden gidelim.
//Services klasörü ekledik. ardından ServiceList adlı bir sınıf ekledik bu klasöre.
//Bu sınıf ViewComponent'i miras aldı ve serviceManager sınıfından bir nesne türetip onun üzerinden işlemlerimize devam edeceğiz.
//Bu nesne ServiceManager serviceManager = new ServiceManager(new EfServiceDal() ); seklinde üretilir çünkü, Db işlemleri entity framework dal üzerinden gerçekleşmektedir.
//Sonra Invoke adlı bir Viewcomponentresult oluşturulur. (Invoke olarak adlandırması gerekiyor) Burada values değişkenine servicemanager.
//TGetList() metodu ile elde edilen veriler atanır ve return View ile View'a gönderilir.
