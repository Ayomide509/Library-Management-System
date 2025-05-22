using System.Globalization;
using System.Text;
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.Worker.Utils
{
    public static class CsvExporter
    {
        public static async Task ExportArchivedBooksAsync(List<Book> books, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id,Title,AuthorId,PublicationYear,ArchivedAt");

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Id},\"{book.Title}\",{book.AuthorId},{book.PublicationYear},{DateTime.UtcNow.ToString("s", CultureInfo.InvariantCulture)}");
            }

            // Ensure directory exists
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            await File.WriteAllTextAsync(filePath, sb.ToString());
        }
    }
}
