using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface ICsvDataImporter
    {
        Task Import();
    }

    public class CsvClass
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string Temperature { get; set; }

        public string TemperatureMax { get; set; }

        public string TemperatureMin { get; set; }

        public string Rainfall { get; set; }
    }
}
