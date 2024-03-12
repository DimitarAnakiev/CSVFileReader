using CSVFileReader.Models;

namespace CSVFileReader.Repositories
{

    public interface ICSVFileRepository
    {
        Task<DataFile> GetMostRecentAsync();
        Task<IEnumerable<DataFile>> GetAllAsync();
        Task AddAsync(DataFile entity);
    }
}
