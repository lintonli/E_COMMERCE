
using Newtonsoft.Json;
using ORDERSERVICE.Models.Dtos;
using ORDERSERVICE.Service.Iservice;

namespace ORDERSERVICE.Service
{
    public class CartService : ICart
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CartDto> GetCartById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Cart");
            var response = await client.GetAsync($"{Id}");
            var context = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(context);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseDto.Result));
            }
            return null;
        }
    }
}
