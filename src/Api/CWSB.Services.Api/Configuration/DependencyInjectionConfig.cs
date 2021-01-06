using CWSB.Core.RabbitMQ;
using CWSB.Core.User;
using CWSB.Services.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CWSB.Services.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddTransient<IChatHub, ChatHub>();
        }
    }
}
