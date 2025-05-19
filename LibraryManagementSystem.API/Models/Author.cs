using System.Text.Json.Serialization;
using LibraryManagementSystem.API.DTOs;

namespace LibraryManagementSystem.API.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public required string Name { get; set; }
        public int BirthYear { get; set; }

        public List<Book>? Books { get; set; }
    }
}
