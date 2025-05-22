using System.Text.Json.Serialization;
using LibraryManagementSystem.Core.DTOs;

namespace LibraryManagementSystem.Core.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public required string Name { get; set; }
        public int BirthYear { get; set; }

        public List<Book>? Books { get; set; }
    }
}
