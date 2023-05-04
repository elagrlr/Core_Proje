using DataAccessLayer.Abstract;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAboutDal:GenericRepository<About>,IAboutDal//ileride olur da crud op. dışında ayrı bi işlem gerekirse ve bu işlem sadece bir entitiyi ilgilendiren bir değişiklik olursa,bu değişiklik IAboutDal'da imzası atılıp burada doldurulacağı için IAboutDal da miras alınmaktadır.
    {
    }
}
