using cowsumer3.Dto;
using cowsumer3.Storage;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace cowsumer3.Logic
{
    public class Subscriber
    {
       private readonly IStorage _storage;
        public Subscriber(IStorage storage)
        {
            _storage = storage;
        }

        public static void ListentoCowLocation()
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
                    Console.WriteLine("Location modtaget [x] Received {0}", message);
                    var dto = JsonConvert.DeserializeObject<LocationCreate>(message);

                    //her burde man bruge Istorage i stedet, men jeg kunne ikke får de til at funke
                    InMemoryStorage inMemory = new InMemoryStorage();
                    inMemory.LocationCreateUpdate(dto.EarTag, dto.Latitude, dto.Longitude);

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

        public static void ListentoCowCreation()
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
                    Console.WriteLine("Ko modtaget [x] Received {0}", message);
                    var dto = JsonConvert.DeserializeObject<CowData>(message);


                    //her burde man bruge Istorage i stedet, men jeg kunne ikke får de til at funke
                    InMemoryStorage inMemory = new InMemoryStorage();
                   
                    inMemory.StoreCow(dto.EarTag, dto.Name, dto.Birthday);

                    Console.WriteLine(" [x] Done");

                    // Note: it is possible to access the channel via
                    //       ((EventingBasicConsumer)sender).Model here
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "cow_queue",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

    }
}
