using System.Text.Json.Serialization;

namespace LibraryManagementSystem.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int PublicationYear { get; set; }
        public required string ISBN {  get; set; }
        public bool IsArchived { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }


    }
}
