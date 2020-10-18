
using cowsumer3.Dto;
using cowsumer3.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace cowsumer3.Controllers
{
    /// <summary>
    /// Concern: To receive and handle request from client.
    /// </summary>
    public class LocationController : Controller
    {
        private readonly IStorage _storage;


        // Concern: Constructor receives dependencies
        public LocationController(IStorage storage)
        {
            _storage = storage;

        }

        [HttpGet("/cowsumer/Report/{earTag}/")]
        public IActionResult LocationReport(string earTag)
        {
            
           var locations = _storage.LocationRead(earTag);
            var cow = _storage.ReadCow(earTag);

            var dto = new LocationReport
            {
                Name = cow.Name,
                Locations = locations
            };

            return Ok(dto);
        }



    }
}