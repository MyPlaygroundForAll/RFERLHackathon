using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Application.ViewModels
{
    public class DataViewModel
    {
        public DateTime Timeline { get; set; }

        public string Temperature { get; set; }

        public string MaximumTemperature { get; set; }

        public string MinimumTemperature { get; set; }

        public string Rainfall { get; set; }
    }
}
