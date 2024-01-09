using Newtonsoft.Json;
using CARTSERVICE.Models.Dtos;
using CARTSERVICE.Services.IServices;

namespace ORDERSERVICE.Service
{
    public class CouponService : ICoupon

    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCouponByCouponCode(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"{couponCode}");
            var context = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(context);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
            }
            return null;
        }
    }
}
