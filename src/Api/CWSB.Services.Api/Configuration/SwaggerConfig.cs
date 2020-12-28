using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CWSB.Services.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                o => o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chat With Stocks Bot",
                    Description = "This APi will allow users to chat and call a bot to get stocks information.",
                    Contact = new OpenApiContact() { Name = "Douglas Mariano Silverio", Email = "douglas072gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licences/MIT") }
                }));
            return services;
        }

        public static IApplicationBuilder UserSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            return app;
        }



    }
}
