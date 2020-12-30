using CWSB.Core.Extensions;
using CWSB.Core.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockBot.Service;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockBot
{
    class Program
    {
        static void Main(string[] args)
        {

    //        var builder = new ConfigurationBuilder()
    //.SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile($"appsettings.json");
    //        _configuration = builder.Build();

            
    //        var rabbitMQConfigurations = new RabbitMQConfigurations();
    //        new ConfigureFromConfigurationOptions<RabbitMQConfigurations>(
    //            _configuration.GetSection("RabbitMQConfigurations"))
    //                .Configure(rabbitMQConfigurations);

            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
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

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, eventArgs) =>
                {
                    var body = eventArgs.Body.Span;
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine(Environment.NewLine + "[New message received] " + message);


                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var post = JsonSerializer.Deserialize<Post>(message, options);

                    if (post.IsCommand())
                    {
                        IStockService service = new StockService();
                        var stockCode = post.GetStockFromCommand();

                        var stockInfo = Task.FromResult(service.GetStock(stockCode));
                    }



                };

                channel.BasicConsume(queue: "tests",
                     autoAck: true,
                     consumer: consumer);

                Console.WriteLine("Waiting messages to proccess");
                Console.WriteLine("Press some key to exit...");
                Console.ReadKey();
            }



        }
    }
}
