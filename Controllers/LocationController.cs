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
			if (city == "all")
			{
				var locations = await _db.Locations.ToListAsync();
				return Ok(locations);
			}

			var getCity = await _db.Locations.FirstOrDefaultAsync(c => c.City.ToLower() == city.ToLower());
			if (getCity == null)
			{
				return NotFound();
			}
			return Ok(getCity);
		}

		[HttpDelete("delete/{city}")]
		public async Task<IActionResult> DeleteLocation(string city)
		{
			var locationToDelete = await _db.Locations.FirstOrDefaultAsync(c => c.City.ToLower() == city.ToLower());

			if (locationToDelete == null)
			{
				return NotFound("Location not found, try again.");
			}

			_db.Locations.Remove(locationToDelete);

			await _db.SaveChangesAsync();

			return Ok($"Location {locationToDelete.City} successfully deleted.");
		}

		[HttpPost]
		public async Task<IActionResult> PostLocation(Location l)
		{
			var locationExists = await _db.Locations.AnyAsync(location => location.City.ToLower() == l.City.ToLower());

			if (locationExists)
				return Problem("Location already exists in database.", statusCode: 409);

			await _db.Locations.AddAsync(l);
			await _db.SaveChangesAsync();

			return Created("api/v1/location", l);
		}
	}
}
