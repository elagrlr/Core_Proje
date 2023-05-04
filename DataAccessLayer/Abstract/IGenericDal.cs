using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T: class
    {
        // Ekleme
        void Insert(T t);

        // Silme
        void Delete(T t);
        
        // Güncelleme
        void Update(T t);

        // Listeleme
        List<T> GetList();
        
        // ID'ye göre getirme
        T GetByID(int id); 
        
        //Filtreye göre listeleme. 
        List<T> GetByFilter(Expression<Func<T, bool>> filter); //System.Linq.Expression'lardan yararlanarak bu filtrelemeyi yapacağız. Expression içinde bir function tanımlıyoruz;filter adındaki bu fonksiyon, T tipinde değer alan ve bool tipinde değer döndüren bir fonksiyondur.

    }
}
