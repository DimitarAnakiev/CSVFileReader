namespace CSVFileReader.Models
{
    public class CsvRecord
    {
        public required string Color { get; set; }
        public required string Value { get; set; }
        public required string Legend { get; set; }

        // Custom validation logic
        public bool IsValid()
        {
            if(Int32.TryParse(Value, out var parsedValue))
            {
                return !string.IsNullOrEmpty(Color) && parsedValue > 0 && parsedValue < 100 && !string.IsNullOrEmpty(Legend);
            }
            return false; 
        }
    }
}
