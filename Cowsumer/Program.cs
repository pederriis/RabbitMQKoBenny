using Cowsumer.dto;
using Cowsumer.Storage;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Dynamic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Cowsumer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare(queue: "location_queue",
                //                     durable: true,
                //                     exclusive: false,
                //                     autoDelete: false,
                //                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    InMemoryStorage storage = new InMemoryStorage();

                    var locationCreate = JsonConvert.DeserializeObject<LocationCreate>(message);
                    storage.LocationCreateUpdate(locationCreate.EarTag,locationCreate.Latitude, locationCreate.Longitude);

                    Console.WriteLine(" [x] Done");

                    // Note: it is possible to access the channel via
                    //       ((EventingBasicConsumer)sender).Model here
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "location_queue",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
