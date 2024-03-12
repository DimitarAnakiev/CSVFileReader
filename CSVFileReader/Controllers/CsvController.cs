using CSVFileReader.Models;
using CSVFileReader.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CSVFileReader.Controllers
{
    public class CsvController : Controller
    {
       private ICSVFileRepository _csvFileRepository;

        public CsvController(ICSVFileRepository csvFileRepository)
        {
            _csvFileRepository = csvFileRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<DataFile> dataFiles = await _csvFileRepository.GetAllAsync();
            return View(dataFiles);
        }

        public IActionResult Upload()
        {
            return View();
        }

        public async Task<IActionResult> Details()
        {
            // Retrieve the most recent DataFile
            DataFile mostRecentDataFile = await _csvFileRepository.GetMostRecentAsync();

            // Ensure a most recent DataFile is found
            if (mostRecentDataFile == null)
            {
                return View("Upload");
            }

            return View(mostRecentDataFile.Records.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> ReadFile(IFormFile fileInput)
        {
            // Just in case check for missing file
            if (fileInput != null && fileInput.Length > 0)
            {
                var records = new List<Record>();

                // Get the valid records out of the DataFile and into records list
                using (var stream = new MemoryStream())
                {
                    await fileInput.CopyToAsync(stream);
                    stream.Position = 0;

                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false,
                        DetectDelimiter = true,
                        IgnoreBlankLines = true,
                        TrimOptions = TrimOptions.Trim
                    };
                    var reader = new StreamReader(stream);
                    var csv = new CsvReader(reader, config);
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<CsvRecord>();

                        // Perform record-level validation
                        if (record.IsValid())
                        {
                            var newRecord = new Record() { Color = record.Color, Legend = record.Legend, Value = short.Parse(record.Value) };
                            records.Add(newRecord);
                        }
                    }
                }
                var file = new DataFile() { FileName = fileInput.FileName, DateUploaded = DateTime.UtcNow, Records = records };
                await _csvFileRepository.AddAsync(file);
                return RedirectToAction("Index", "Csv");
            }
            else
            {
                return View("Upload");
            }
        }
    }
}
