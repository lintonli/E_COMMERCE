using CARTSERVICE.Models.Dtos;

namespace CARTSERVICE.Services.IServices
{
    public interface ICoupon
    {
        Task<CouponDto> GetCouponByCouponCode(string couponCode);
    }
}
