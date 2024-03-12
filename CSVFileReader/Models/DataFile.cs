using System.ComponentModel.DataAnnotations;

namespace CSVFileReader.Models
{
    public class DataFile
    {
        [Key]
        public int Id { get; set; } // Primary key
        public required string FileName { get; set; }
        public DateTime DateUploaded { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
