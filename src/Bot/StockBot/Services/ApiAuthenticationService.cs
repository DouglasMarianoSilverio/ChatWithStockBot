using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Core.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockBot.Services
{
    public class ApiAuthenticationService : Service, IApiAuthenticationService
    {
        public ApiAuthenticationService()
        {            
        }

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339");
            

            var response = await client.PostAsync("/api/identity/login", loginContent);
            
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
