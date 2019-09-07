using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Application.ViewModels
{
    public class DataReportViewModel
    {
        public List<DateTime> Timelines { get; set; }

        public DataReportDetail Data { get; set; }

        public class DataReportDetail
        {
            public List<string> Temperatures { get; set; }

            public List<string> MaximumTemperatures { get; set; }

            public List<string> MinimumTemperatures { get; set; }

            public List<string> Rainfalls { get; set; }
        }
    }
}
