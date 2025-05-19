namespace LibraryManagementSystem.API.DTOs
{
    public class CreateBookDTO
    {
        public required string Title { get; set; }
        public int PublicationYear { get; set; }
        public required string ISBN { get; set; }
        public int AuthorId { get; set; }
    }
}
