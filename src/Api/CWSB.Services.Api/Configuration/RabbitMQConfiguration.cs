using CWSB.Core.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CWSB.Services.Api.Configuration
{
    public static class RabbitMQConfiguration
    {
        public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQConfigurationsSection = configuration.GetSection("AppSettings");
            services.Configure<RabbitMQConfigurations>(rabbitMQConfigurationsSection);
            return services;
        }
       
    }
}
