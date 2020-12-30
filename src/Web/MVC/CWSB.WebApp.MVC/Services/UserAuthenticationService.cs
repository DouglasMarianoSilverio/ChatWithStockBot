using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Core.Services;
using CWSB.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Services
{
    public class UserAuthenticationService : Service, IUserAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;      
        



        public UserAuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            httpClient.BaseAddress = new Uri(_settings.ServicesUrl);
        }

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
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

        public async Task<UserLoginResponse> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/identity/new-user", registerContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

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
