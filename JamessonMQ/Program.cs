using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamessonMQ
{
    class Program
    {
        private static void Main(string[] args)
        {

             //SendMessage();
             ReceivedMessage();
        }



        private static void ReceivedMessage()
        {
            var factory = new global::RabbitMQ.Client.ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "JamesQueue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new global::RabbitMQ.Client.Events.EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;

                    var message = global::System.Text.Encoding.UTF8.GetString(body);
                    global::System.Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "JamesQueue", autoAck: true, consumer: consumer);

                global::System.Console.WriteLine(" Press [enter] to exit.");
                global::System.Console.ReadLine();
            }
        }



        static void SendMessage()
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    for (int i = 0; i < 100; i++ )
                    {
                        channel.QueueDeclare(queue: "JamesQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                        string message = "Primeiro Teste";
                        var body = Encoding.UTF8.GetBytes(message);
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        channel.BasicPublish(exchange: "", routingKey: "PAUL", mandatory: true, basicProperties: properties, body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }

                }
            }
        }
    }
}