using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherApiWorkshop.Controllers
{

    [Route("[controller]")]
    public class LocationController : Controller
    {
        List<Model> cities = new();

        Model location1 = new() {Id = 1, Country = "Sweden", Region = "Stockholm", City = "Stockholm", Latitude = "59.3294", Longitude="18.0686"};
        Model location2 = new() {Id = 2, Country = "Sweden", Region = "Västra Götaland", City = "Göteborg", Latitude = "59.3294", Longitude="18.0686"};
        Model location3 = new() {Id = 1, Country = "Sweden", Region = "Skåne", City = "Malmö", Latitude = "55.6058", Longitude="13.0358"};


        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

public class Model
{
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string Region { get; set; }
    public required string City { get; set; }
    public required string Latitude { get; set; }
    public required string Longitude { get; set; }

}