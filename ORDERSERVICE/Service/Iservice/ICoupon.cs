using ORDERSERVICE.Models.Dtos;

namespace ORDERSERVICE.Service.Iservice
{
    public interface ICoupon
    {
        Task<CouponDto> GetCouponByCouponCode(string couponCode);
    }
}
