using System;

namespace LocationWebApp.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string TerminalId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateTime { get; set; }
    }
}
