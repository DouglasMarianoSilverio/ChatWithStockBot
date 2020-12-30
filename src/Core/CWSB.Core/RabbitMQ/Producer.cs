using CWSB.Core.Models;
using CWSB.Core.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CWSB.Core.RabbitMQ
{
    public class ProducerService : Service, IProducerService
    {
        private readonly RabbitMQConfigurations _rabbitMQConfigurations;

        public ProducerService(IOptions<RabbitMQConfigurations>   rabbitMQConfigurations)
        {
            this._rabbitMQConfigurations = rabbitMQConfigurations.Value;
        }

        public async Task Produce(Post postMessage)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitMQConfigurations.HostName,
                Port = _rabbitMQConfigurations.Port,
                UserName = _rabbitMQConfigurations.UserName,
                Password = _rabbitMQConfigurations.Password,
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "tests",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var messageContent = await GetContent(postMessage).ReadAsStringAsync();

                var body = Encoding.UTF8.GetBytes(messageContent);

                channel.BasicPublish(exchange: "",
                                     routingKey: "tests",
                                     basicProperties: null,
                                     body: body);
            }
        }

    }
}
