using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSVFileReader.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; } // Primary key
        public string Color { get; set; }
        public int Value { get; set; }
        public string Legend { get; set; }

        // Foreign key to CsvFile
        [ForeignKey("DataFile")]
        public int FileId { get; set; }
        public virtual DataFile DataFile { get; set; }
    }
}
