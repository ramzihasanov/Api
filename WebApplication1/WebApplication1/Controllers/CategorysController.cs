using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.DTOs.BookDtos;
using WebApplication1.DTOs.ProductDtos;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly AppDbContext _appDb;

        public CategorysController(AppDbContext appDb)
        {
            this._appDb = appDb;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<Category> categories = _appDb.Catagories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cat = _appDb.Catagories.Find(id);
            if (cat is null) return NotFound();
            GetCategoryDto dto = new GetCategoryDto()
            {          
                Name = cat.Name,
            };

            return Ok(dto);

        }

        [HttpPost]
        public IActionResult Create(CategoryCreateDto dto)
        {
            Category catagory = new Category
            {
                Name = dto.Name,
            };
            catagory.CreatedDate = DateTime.UtcNow.AddHours(4);
            catagory.UpdatedDate = DateTime.UtcNow.AddHours(4);
            catagory.IsDeleted = false;
            _appDb.Catagories.Add(catagory);
            _appDb.SaveChanges();

            return Ok("yarandi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCategoryDto dto)
        {
            var categoryToUpdate = _appDb.Catagories.FirstOrDefault(x => x.Id == id);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = dto.Name;
                categoryToUpdate.CreatedDate = DateTime.UtcNow.AddHours(4);
                _appDb.SaveChanges();
                return Ok(categoryToUpdate);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var catagoryToDelete = _appDb.Catagories.FirstOrDefault(x => x.Id == id);

            if (catagoryToDelete != null)
            {
                _appDb.Remove(catagoryToDelete);
                _appDb.SaveChanges();
                return Ok("Catagory deleted");
            }

            return NotFound();
        }
        [HttpPatch("{id}")]
        public IActionResult SoftDelete(int id)
        {
            var catagoryToSoftDelete = _appDb.Catagories.FirstOrDefault(x => x.Id == id);

            if (catagoryToSoftDelete != null)
            {
                catagoryToSoftDelete.IsDeleted = true;
                _appDb.SaveChanges();
                return Ok("Successfully soft deleted ");
            }

            return NotFound();
        }
    }
}