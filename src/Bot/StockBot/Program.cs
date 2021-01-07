using CWSB.Core.Extensions;
using CWSB.Core.Models;
using CWSB.Core.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockBot.Configurations;
using StockBot.Model;
using StockBot.Services;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockBot
{
    class Program
    {
        private static IConfiguration _configuration;
        private static IChatAPiServiceClient _chatApiServiceClient;
        private static IApiAuthenticationService _apiAuthenticationService;


        

        static void Main(string[] args)
        {



            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json");

            _configuration = builder.Build();

            var rabbitMQConfigurations = new RabbitMQConfigurations();
            LoadRabbitMQConfig(rabbitMQConfigurations);

            var botConfiguration = new BotConfiguration();
            LoadBotConfig(botConfiguration);

            _chatApiServiceClient = new ChatApiServiceClient(botConfiguration);
            _apiAuthenticationService = new ApiAuthenticationService(botConfiguration);

            var user = new UserLogin { Email = botConfiguration.User, Password = botConfiguration.Password };
            var userToken = _apiAuthenticationService.Login().Result;
            ConnectionFactory connectionFactory = GetConnectionFactory(rabbitMQConfigurations);

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
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
                Post post = DeserializeObject(message, options);
                
                if (post.IsInvalidCommand())
                {
                    var result = _chatApiServiceClient.SendMessage(new PostCreateRequest { Message = $"{post.User} you must enter a Symbol to use the command, like /stock=xp.us" }, userToken).Result;
                }

                else if (post.IsCommand())
                {
                    IStockService service = new StockService();
                    var stockCode = post.GetStockFromCommand();

                    var stockInfo = service.GetStock(stockCode).Result;

                    var msg_ = stockInfo.GetStockQuoteFormatted();

                    var result = _chatApiServiceClient.SendMessage(new PostCreateRequest { Message = msg_ }, userToken).Result;
                }

                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

            };

            channel.BasicConsume(queue: "tests",
                 autoAck: false,
                 consumer: consumer);

            Console.WriteLine("Waiting messages to proccess");
            Console.WriteLine("Press some key to exit...");
            Console.ReadKey();



        }

        private static ConnectionFactory GetConnectionFactory(RabbitMQConfigurations rabbitMQConfigurations)
        {
            return new ConnectionFactory()
            {
                HostName = rabbitMQConfigurations.HostName,
                Port = rabbitMQConfigurations.Port,
                UserName = rabbitMQConfigurations.UserName,
                Password = rabbitMQConfigurations.Password
            };
        }

        private static void LoadRabbitMQConfig(RabbitMQConfigurations rabbitMQConfigurations)
        {
            new ConfigureFromConfigurationOptions<RabbitMQConfigurations>(
                            _configuration.GetSection("RabbitMQConfigurations"))
                                .Configure(rabbitMQConfigurations);
        }

        private static void LoadBotConfig(BotConfiguration botConfiguration)
        {
            new ConfigureFromConfigurationOptions<BotConfiguration>(
                            _configuration.GetSection("BotConfigurations"))
                                .Configure(botConfiguration);
        }

        private static Post DeserializeObject(string message, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<Post>(message, options);
        }
    }
}
