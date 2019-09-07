using CsvHelper;
using Hackathon.Application.Interfaces;
using Hackathon.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hackathon.Application.Services
{
    public class CsvDataImporter : ICsvDataImporter
    {
        private readonly IDataRepository _dataRepository;

        public CsvDataImporter(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public async Task Import()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "data.csv");
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<CsvClass>();
                foreach (var record in records)
                {
                    var timelineDate = new DateTime(record.Year, record.Month, 1);
                    var newData = Data.Create(timelineDate, record.Temperature, record.TemperatureMin, record.TemperatureMax, record.Rainfall);
                    _dataRepository.Add(newData);
                }
            }

            await _dataRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
