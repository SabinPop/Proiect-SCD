using LocationAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAPI.DataContext
{
    public class LocationContext : DbContext
    {
        public LocationContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Location> Locations { get; set; }
    }
}
