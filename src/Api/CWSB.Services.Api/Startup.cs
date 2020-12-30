using CWSB.Services.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CWSB.Services.Api
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            //be ready for multiple enviorments.
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile(path: "appsetttings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            //if (hostEnvironment.IsDevelopment())
            //{
            //    builder.AddUserSecrets<Startup>();
            //}

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIndentityConfiguration(Configuration);

            services.AddApiConfiguration();

            services.AddSwaggerConfiguration();

            services.RegisterServices();

            services.AddRabbitMQConfiguration(Configuration);


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UserSwaggerConfiguration();

            app.UseApiConfiguration(env);


        }
    }
}
