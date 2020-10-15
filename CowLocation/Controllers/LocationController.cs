using CowLocation.Dto;
using CowLocation.InterService;
using CowLocation.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace CowLocation.Controllers
{
    /// <summary>
    /// Concern: To receive and handle request from client.
    /// </summary>
    public class LocationController : Controller
    {
        private readonly IStorage _storage;
        private readonly MasterData _masterDataService;

        // Concern: Constructor receives dependencies
        public LocationController(IStorage storage, MasterData masterDataService)
        {
            _storage = storage;
            _masterDataService = masterDataService;
        }

        /// <summary>
        /// API to receive ear tag and GRP coordinates from cow
        /// </summary>
        /// <param name="dto">Input data</param>
        /// <returns></returns>
        [HttpPost("/Location/Create/")]
        public IActionResult LocationCreate([FromBody] LocationCreate dto)
        {
            try
            {
                //noget kode her der smider det op i rabbiten
                // _storage.LocationCreateUpdate(dto.EarTag, dto.Latitude, dto.Longitude);

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "location_queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonConvert.SerializeObject(dto);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "location_queue",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }

             



                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// API to report latest position of a specifike cow
        /// </summary>
        /// <param name="earTag">Ear tag of the cow</param>
        /// <returns>LocationReport that contains name and GPS coordinates of the cow.</returns>
        [HttpGet("/Location/Report/{earTag}/")]
        public IActionResult LocationReport(string earTag)
        {
            var cowName = _masterDataService.GetCowName(earTag);
            var location = _storage.LocationRead(earTag);

            var dto = new LocationReport
            {
                Name = cowName,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            return Ok(dto);
        }
    }
}