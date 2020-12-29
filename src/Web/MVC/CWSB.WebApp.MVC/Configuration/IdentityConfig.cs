using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CWSB.WebApp.MVC.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIndentityConfiguration(this IServiceCollection services )
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/access-denied";
                });            

        }

        public static void UseIndentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();            
        }
    }
}
