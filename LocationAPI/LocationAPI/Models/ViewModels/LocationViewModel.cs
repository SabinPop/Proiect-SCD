using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAPI.Models.ViewModels
{
    public class LocationViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TerminalId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
