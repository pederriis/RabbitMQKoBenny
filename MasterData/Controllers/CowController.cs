using MasterData.Dto;
using MasterData.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MasterData.Controllers
{
    /// <summary>
    /// Concern: To receive and handle request from client.
    /// </summary>
    public class CowController : Controller
    {
        private readonly IStorage _storage;

        // Concern: Constructor receives dependencies
        public CowController(IStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// API to receive data needed to create the "cow account"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Ok if saved and BadRequest if not</returns>
        [HttpPost("/Cow/Create/")]
        public IActionResult CowCreate([FromBody] CowCreate dto)
        {
            try
            {
                // _storage.Store(dto.EarTag, dto.Name, dto.Birthday);
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    //channel.QueueDeclare(queue: "cow_queue",
                    //                     durable: false,
                    //                     exclusive: false,
                    //                     autoDelete: false,
                    //                     arguments: null);

                    string message = JsonConvert.SerializeObject(dto);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "cow_queue",
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
        /// API to get the data on a cow with a specifike ear tag
        /// </summary>
        /// <param name="earTag"></param>
        /// <returns>CowRead containing name and birthday</returns>
        [HttpGet("/Cow/Read/{earTag}/")]
        public IActionResult CowRead(string earTag)
        {
            try
            {
                var cow = _storage.Read(earTag);

                var dto = new CowRead
                {
                    Name = cow.Name,
                    Birthday = cow.Birthday
                };

                return Ok(dto);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
