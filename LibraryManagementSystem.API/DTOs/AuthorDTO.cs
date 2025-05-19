namespace LibraryManagementSystem.API.DTOs
{
    public class AuthorDTO
    {
        public int AuthorId { get; set; }
        public required string Name { get; set; }
        public int BirthYear { get; set; }

         
    }
}
