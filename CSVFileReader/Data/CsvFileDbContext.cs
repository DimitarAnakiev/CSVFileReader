using CSVFileReader.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVFileReader.Data
{
    public class CSVFileDbContext : DbContext
    {
        public CSVFileDbContext(DbContextOptions<CSVFileDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<DataFile> Files { get; set; }
    }
}
