
using LocationAPI.Models;
using LocationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LocationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        // GET: api/<LocationController>
        [HttpGet("all")]
        public ActionResult<IEnumerable<Location>> GetLocations()
        {
            return Ok(locationService.GetLocations());
        }

        //POST: api/<LocationController>/date-time-interval
        [HttpPost("get-from-date-time-interval")]
        public ActionResult<IEnumerable<Location>> GetLocationsBetweenDateTimeInterval([FromBody] DateTimeInterval interval)
        {
            return Ok(locationService.GetLocationsInDateTimeInterval(interval.DateTimeStart, interval.DateTimeEnd));
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public ActionResult<Location> GetLocationById(int id)
        {
            var location = locationService.GetLocationById(id);
            if (location == null)
                return NotFound("LocationId provided does not exist");
            return Ok(location);
        }

        // POST api/<LocationController>/create
        [HttpPost("create")]
        public ActionResult AddLocation([FromBody] Location location)
        {
            var isCreated = locationService.AddLocation(location);
            if (isCreated)
                return Ok(location);
            return BadRequest("Could not add location");
        }

        // PUT api/<LocationController>/update
        [HttpPut("update")]
        public ActionResult UpdateLocation([FromBody] Location location)
        {
            if (!locationService.LocationExists(location.Id))
                return NotFound("Could not find location");
            var isUpdated = locationService.UpdateLocation(location);
            if (isUpdated)
                return Ok(location);
            return BadRequest("Could not update location");
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!locationService.LocationExists(id))
                return NotFound("Could not find location");
            var isDeleted = locationService.DeleteLocationById(id);
            if (isDeleted)
                return Ok("Location was deleted");
            return BadRequest("Could not delete location");
        }
    }
}
