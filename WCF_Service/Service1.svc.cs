using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_Service
{
    public class Service1 : IService1
    {
        public MessageType ReceivedMessage()
        {
            return new MessageType() ;
        }

        public MessageType SendMessage(MessageType composite)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = "JamesQueue";
                    channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(composite);
                    var body = Encoding.UTF8.GetBytes(message);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(exchange: "", routingKey: queue, mandatory: true, basicProperties: properties, body: body);
                    Console.WriteLine(" [x] Sent {0}", message);


                }
            }

            return composite;
        }

    }
}
