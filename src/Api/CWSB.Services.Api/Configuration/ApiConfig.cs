using CWSB.Services.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace CWSB.Services.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            
            var corsUrls = configuration.GetSection("Cors:CorsOrigins").Value.ToString()
                      .Split(",", StringSplitOptions.RemoveEmptyEntries)
                             .Select(o => o.Trim('/'))
                             .ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithOrigins(corsUrls)
                            .SetIsOriginAllowed((host) => true)
                            );
            });
            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("Total");

            app.UseHttpsRedirection();

            app.UseRouting();

            

            app.UseIndentityConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSignalR();
            });

            return app;
        }
    }
}
