namespace LibraryManagementSystem.Core.DTOs
{
    public class AuthorDetailDTO
    {
        public int AuthorId { get; set; }
        public required string Name { get; set; }
        public int BirthYear { get; set; }

        public List<BookDTO>? Books { get; set; }
    }
}
