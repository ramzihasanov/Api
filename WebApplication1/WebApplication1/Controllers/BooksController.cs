using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.DTOs.BookDtos;
using WebApplication1.DTOs.ProductDtos;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<Book> books = _context.Books.ToList();
            IEnumerable<BookGetDto> bookDtos = new List<BookGetDto>();
            bookDtos = books.Select(x => new BookGetDto { Id = x.Id, Name = x.Name, Price = x.Price });
            return Ok(bookDtos);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _context.Books.Find(id);
            if (book is null) return NotFound();
            BookGetDto dto = new BookGetDto()
            {
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
            };

            return Ok(dto);

        }   
        [HttpPost]
        public IActionResult Create(BookCreateDto dto)
        {
            Book book = new Book()
            {
                Name = dto.Name,
                Price = dto.Price,
                CostPrice = dto.CostPrice,
            };
            book.CreatedDate = DateTime.UtcNow.AddHours(4);
            book.UpdatedDate = DateTime.UtcNow.AddHours(4);
            book.IsDeleted = false;
            _context.Books.Add(book);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Object yaradildi" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBookDto updateDto)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(x => x.Id == id);

            if (bookToUpdate != null)
            {
                bookToUpdate.Name = updateDto.Name;           
                _context.SaveChanges();
                return Ok(bookToUpdate);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                _context.Remove(book);
                _context.SaveChanges();
                return Ok("Successfully Delete");
            }

            return NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult SoftDelete(int id)
        {
            var bookToSoftDelete = _context.Books.FirstOrDefault(x => x.Id == id);

            if (bookToSoftDelete != null)
            {
                bookToSoftDelete.IsDeleted = true;
                _context.SaveChanges();
                return Ok("Successfully soft deleted ");
            }

            return NotFound();
        }





    }
}