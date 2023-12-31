using CARTSERVICE.Models.Dtos;
using CARTSERVICE.Services.IServices;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace CARTSERVICE.Services
{
    public class ProductService : IProduct
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ProductDto> GetProductById(Guid ProductId)
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"{ProductId}");
            var context =await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(context);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
            }
            return null;
        }
    }
}
