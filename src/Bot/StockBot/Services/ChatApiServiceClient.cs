using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Core.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockBot.Services
{
    public class ChatApiServiceClient : Service, IChatAPiServiceClient
    {
        public async Task<PostCreateResponse> SendMessage(PostCreateRequest request, UserLoginResponse userToken)
        {
            var loginContent = GetContent(request);

            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44339")
            };

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken.AccessToken);//  Add("Authorization", $"Bearer:{userToken.AccessToken}");


            var response = await client.PostAsync("/api/chat/new-message", loginContent);

            if (!HandleErrorsResponse(response))
            {
                return new PostCreateResponse
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<PostCreateResponse>(response);
        }

    }
}
