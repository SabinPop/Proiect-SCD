using LocationAPI.DataContext;
using LocationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly LocationContext context;

        public LocationService(LocationContext context)
        {
            this.context = context;
        }

        public bool AddLocation(Location location)
        {
            if (location == null)
                return false;
            context.Locations.Add(location);
            context.SaveChangesAsync();
            return true;
        }

        public bool DeleteLocationById(int id)
        {
            var location = GetLocationById(id);
            if (location == null)
                return false;
            context.Locations.Remove(location);
            context.SaveChangesAsync();
            return true;
        }

        public Location GetLocationById(int id)
        {
            return context.Locations.FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Location> GetLocations()
        {
            return context.Locations.ToList();
        }

        public IEnumerable<Location> GetLocationsInDateTimeInterval(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            return context.Locations.Where(l => l.DateTime >= dateTimeStart && l.DateTime <= dateTimeEnd).ToList();
        }

        public bool LocationExists(int id)
        {
            return context.Locations.Any(l => l.Id == id);
        }

        public bool UpdateLocation(Location location)
        {
            context.Entry(location).State = EntityState.Modified;
            try
            {
                context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
