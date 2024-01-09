using COUPON.Models;

namespace COUPON.Services.IService
{
    public interface ICoupon
    {
        Task<List<Coupon>>GetAllCoupons();
        Task<Coupon> GetCoupons(Guid Id);
        Task<Coupon> GetCoupon(String code);
        Task<string> AddCoupon(Coupon coupon);  
        Task<string> UpdateCoupon(Coupon Updatedcoupon);

        Task<string> DeleteCoupon(Coupon coupon);
    }
}
