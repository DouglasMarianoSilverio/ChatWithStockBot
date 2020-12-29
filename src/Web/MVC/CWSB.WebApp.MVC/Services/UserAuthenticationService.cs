using CWSB.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly HttpClient _httpClient;
        const string baseServiceUrl = @"https://localhost:44339";

        public UserAuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(userLogin),
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync($"{baseServiceUrl}/api/identity/login", loginContent);
            var teste = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);           
            
        }

        public async Task<UserLoginResponse> Register(UserRegister userRegister)
        {
            var registerContent = new StringContent(
               JsonSerializer.Serialize(userRegister),
               Encoding.UTF8,
               "application/json");
            var response = await _httpClient.PostAsync($"{baseServiceUrl}/api/identity/new-user", registerContent);

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}
