using LocationAPI.Models;
using LocationAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAPI.Services
{
    public interface ILocationService
    {
        public IEnumerable<Location> GetLocations();
        public IEnumerable<Location> GetLocationsInDateTimeInterval(DateTime dateTimeStart, DateTime dateTimeEnd);
        public Location GetLocationById(int id);
        public bool AddLocation(Location location);
        public bool DeleteLocationById(int id);
        public bool UpdateLocation(Location location);
        public bool LocationExists(int id);
    }
}
