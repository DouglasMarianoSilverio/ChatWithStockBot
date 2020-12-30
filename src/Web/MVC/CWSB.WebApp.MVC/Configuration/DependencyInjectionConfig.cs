
using CWSB.Core.User;
using CWSB.WebApp.MVC.Services;
using CWSB.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CWSB.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IUserAuthenticationService, UserAuthenticationService>();
            
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IUserChatService, UserChatService>()
                 .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
        }
    }
}
