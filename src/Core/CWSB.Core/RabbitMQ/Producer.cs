using CWSB.Core.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CWSB.Core.RabbitMQ
{
    public  class ProducerService : Service
    {


        public Task Produce(string message)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                while (true)
                {                    

                    var teste = Console.ReadLine();

                    channel.QueueDeclare(
                        queue: "tests",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                     message =
                        $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - " +
                        $"Message content: {teste}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "tests",
                                         basicProperties: null,
                                         body: body);
                }
            }
        }

    }
}
