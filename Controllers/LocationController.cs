using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherApiWorkshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        List<Location> cities = [];
        Location location1 = new() {Id = 1, Country = "Sweden", Region = "Stockholm", City = "Stockholm", Latitude = "59.3294", Longitude="18.0686"};
        Location location2 = new() {Id = 2, Country = "Sweden", Region = "Västra Götaland", City = "Göteborg", Latitude = "59.3294", Longitude="18.0686"};
        Location location3 = new() {Id = 1, Country = "Sweden", Region = "Skåne", City = "Malmö", Latitude = "55.6058", Longitude="13.0358"};
        public LocationController()
        {
            cities.Add(location1);
            cities.Add(location2);
            cities.Add(location3);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(cities);
        }

    }
}
