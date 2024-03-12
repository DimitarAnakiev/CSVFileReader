using CSVFileReader.Data;
using CSVFileReader.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVFileReader.Repositories
{
    public class CsvFileRepository : ICSVFileRepository
    {
        protected readonly CSVFileDbContext _context;

        public CsvFileRepository(CSVFileDbContext context)
        {
            _context = context;
        }

        public async Task<DataFile> GetMostRecentAsync()
        {
            return await _context.Set<DataFile>()
                        .Include(df => df.Records)
                        .OrderByDescending(df => df.DateUploaded)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataFile>> GetAllAsync()
        {
            return await _context.Set<DataFile>()
                        .OrderByDescending(df => df.DateUploaded)
                        .ToListAsync();
        }

        public async Task AddAsync(DataFile df)
        {
            await _context.Set<DataFile>().AddAsync(df);
            await _context.SaveChangesAsync();
        }
    }
}
