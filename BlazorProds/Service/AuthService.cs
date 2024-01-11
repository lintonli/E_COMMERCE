using BlazorProds.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace BlazorProds.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:7121";
        public AuthService(HttpClient http)
        {
            _httpClient = http;
        }
        public async Task<LoginResponseDto> Login(LogInUser logIn) 
        {
            var request = JsonConvert.SerializeObject(logIn);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/login", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<LoginResponseDto>(results.Result.ToString());

            }
            return new LoginResponseDto();
        }
        public async Task<ResponseDto> Register(User registerRequestDto)
        {
            var request = JsonConvert.SerializeObject(registerRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }
            return new ResponseDto();
        }
    }
}
