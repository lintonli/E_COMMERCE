using Newtonsoft.Json;
using ORDERSERVICE.Models.Dtos;
using ORDERSERVICE.Service.Iservice;

namespace ORDERSERVICE.Service
{
    public class UserService : IUser
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async  Task<UserDto> GetUserById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("User");
            var response = await client.GetAsync($"{Id}");
            var context = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(context);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserDto>(Convert.ToString(responseDto.Result));
            }
            return null;
        }
    }
}
