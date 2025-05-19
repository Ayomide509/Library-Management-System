using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.API.Data;
using LibraryManagementSystem.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {

        private readonly LibraryContext _context;
        public AuthorController(LibraryContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _context.authors.Include(a => a.Books)
                .Select(a => new AuthorDTO
                {
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                    BirthYear = a.BirthYear,
                })
                .ToListAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _context.authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }

            var authorDto = new AuthorDetailDTO
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                BirthYear = author.BirthYear,
                Books = author.Books?.Select(book => new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublicationYear = book.PublicationYear,
                    ISBN = book.ISBN,
                    AuthorId = author.AuthorId,
                }).ToList()



            };

            return Ok(authorDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO dto)
        {
            var author = new Author
            {
                Name = dto.Name,
                BirthYear = dto.BirthYear,
            };
            _context.authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorId }, author);

             
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAuthor(int id ,CreateAuthorDTO dto)
        {
            var author = await _context.authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            author.Name = dto.Name;
            author.BirthYear = dto.BirthYear;

            _context.authors.Update(author);
            await _context.SaveChangesAsync();
            return Ok(author);

        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.authors.FindAsync(id);
            if ( author == null)
            {
                return NotFound();
            }
            _context.authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}
