using LibraryManagementSystem.Core.Data;
using LibraryManagementSystem.Worker.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
 
 

namespace LibraryManagementSystem.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration config)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int archiveThreshold = _config.GetValue<int>("ArchivalSettings:BookAgeThresholdYears", 10);
            int delayMinutes = _config.GetValue<int>("ArchivalSettings:IntervalMinutes", 5);


             

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();

                    _logger.LogInformation("Archiving check started at: {time}", DateTimeOffset.Now);

                    int currentYear = DateTime.Now.Year;
                    var outdatedBooks = await dbContext.Books
                        .Where(b => !b.IsArchived && b.PublicationYear <= currentYear - archiveThreshold)
                        .ToListAsync(stoppingToken);

                    if (outdatedBooks.Any())
                    {
                        foreach (var book in outdatedBooks)
                        {
                            book.IsArchived = true;
                            _logger.LogInformation("Archiving Book: {Id} - {Title}", book.Id, book.Title);
                        }

                        await dbContext.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation($"{outdatedBooks.Count} book(s) archived.");

                        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
                        string fileName = $"ArchivedBooks_{timestamp}.csv";
                        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exports", fileName);

                        await CsvExporter.ExportArchivedBooksAsync(outdatedBooks, filePath);
                        _logger.LogInformation($"Archived books exported to CSV: {filePath}");
                    }
                    else
                    {
                        _logger.LogInformation("No outdated books found.");
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during archival process");
                }

                await Task.Delay(TimeSpan.FromMinutes(delayMinutes), stoppingToken);
            }
        }


    }
}
