using Core_Proje_Api.DAL.ApiContext;
using Core_Proje_Api.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core_Proje_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        /// <summary>
        /// Kategori listesini döndüren fonksiyon. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CategoryList()
        {
            var c = new Context();

            // Sonucun başarılı olduğunu gösterir. 200 kodu ile çalışır.
            return Ok(c.Categories.ToList());
        }

        /// <summary>
        ///  Id parametresi ile çalışan bu fonksiyon, Id'ye göre getirm işlemi yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var c = new Context();
            var value = c.Categories.Find(id);

            if (value == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(value);
            }
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            var c = new Context();
            c.Add(category);
            c.SaveChanges();
            return Created("", category);
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var c = new Context();
            var value = c.Categories.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            else
            {
                c.Remove(value);
                c.SaveChanges();
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var c = new Context();
            var value = c.Find<Category>(category.CategoryId);
            if (value == null)
            {
                return NotFound();
            }
            else
            {
                value.CategoryName = category.CategoryName;
                c.Update(value);
                c.SaveChanges();
                return NoContent();
            }
        }
    }
}
