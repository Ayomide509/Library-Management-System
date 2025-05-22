using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Data;
using LibraryManagementSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string? authorName, [FromQuery] int? publicationYear, [FromQuery] string? sortBy)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrEmpty(authorName))
            {
                query = query.Where(b => b.Author.Name.Contains(authorName));
            }

            if (publicationYear.HasValue)
            {
                query = query.Where(b => b.PublicationYear == publicationYear.Value);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "title":
                        query = query.OrderBy(b => b.Title);
                        break;
                    case "year":
                        query = query.OrderBy(b => b.PublicationYear);
                        break;
                    default:
                        break;
                }
            }

            var books = await query.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                PublicationYear = b.PublicationYear,
                ISBN = b.ISBN,
                AuthorId = b.AuthorId
            }).ToListAsync();

            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            };

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO dto)
        {
            var book = new Book
            {

                Title = dto.Title,
                PublicationYear = dto.PublicationYear,
                ISBN = dto.ISBN,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, CreateBookDTO dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = dto.Title;
            book.PublicationYear = dto.PublicationYear;
           

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return Ok(book);

        }

        [HttpDelete("{id}")]
         public async Task <IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if ( book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();


        }
         
    
    }
}
