using Microsoft.EntityFrameworkCore; 
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Core.Helpers;
namespace LibraryManagementSystem.Core.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Author> authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminUser = new User
            {
                Id = 1,
                UserName = "admin",
                PasswordHash = PasswordHelper.HashPassword("password123"),
                Role = "Admin"
            };

            modelBuilder.Entity<User>().HasData(adminUser);
        }

    }



}

