using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TestimonialManager:ITestimonialService
    {
        ITestImonialDal _testImonialDal;

        public TestimonialManager(ITestImonialDal testImonialDal)
        {
            _testImonialDal = testImonialDal;
        }
        public void TAdd(Testimonial t)
        {
            _testImonialDal.Insert(t);  
        }

        public void TDelete(Testimonial t)
        {
            _testImonialDal.Delete(t);
           
        }

        public Testimonial TGetByID(int id)
        {
            return _testImonialDal.GetByID(id);
        }

        public List<Testimonial> TGetList()
        {
           return _testImonialDal.GetList();
        }

        public void TUpdate(Testimonial t)
        {
            _testImonialDal.Update(t);
        }
        
    }
}
