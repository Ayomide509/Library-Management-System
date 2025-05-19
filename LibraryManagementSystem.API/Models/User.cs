namespace LibraryManagementSystem.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null;
        public string PasswordHash { get; set; } = null;
        public string Role { get; set; } = "User";
    }
}
