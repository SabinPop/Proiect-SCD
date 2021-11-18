using System;
using System.ComponentModel.DataAnnotations;

namespace LocationWebApp.Models.ViewModels
{
    public class LocationViewModel
    {
        [Display(Name = "Terminal ID")]
        public string TerminalId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Display(Name = "Date - Time")]
        public DateTime DateTime { get; set; }
    }
}
