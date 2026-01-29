using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WeatherApiWorkshop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController : Controller
    {
        private readonly AppDbContext _db;
        public LocationController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetCityByName([FromQuery] string? city)
        {
            var getCity = await _db.Locations.FirstOrDefaultAsync(c => c.City.ToLower() == city.ToLower());
            if (getCity == null)
            {
                return NotFound();
            }
            return Ok(getCity);
        }

    }
}
