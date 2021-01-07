using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Core.Services;
using StockBot.Configurations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockBot.Services
{
    public class ApiAuthenticationService : Service, IApiAuthenticationService
    {

        private readonly BotConfiguration _botConfiguration;
        private HttpClient _httpClient;
        public ApiAuthenticationService(BotConfiguration configuration)
        {
            _botConfiguration = configuration;
            _httpClient = new HttpClient() { BaseAddress = new Uri(configuration.ServicesUrl )};
        }

        public async Task<UserLoginResponse> Login()
        {
            var userLogin = new UserLogin { Email = _botConfiguration.User, Password = _botConfiguration.Password };
            var loginContent = GetContent(userLogin);


            var response = await _httpClient.PostAsync("/api/identity/login", loginContent);
            
            if (!HandleErrorsResponse(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserLoginResponse>(response);            
        }

       
    }
}
