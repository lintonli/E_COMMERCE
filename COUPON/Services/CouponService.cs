using COUPON.Data;
using COUPON.Models;
using COUPON.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace COUPON.Services
{
    public class CouponService : ICoupon
    {
        private readonly ApplicationDbContext _context;
        public CouponService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddCoupon(Coupon coupon)
        {
            _context.Coupons.Add(coupon);

            await _context.SaveChangesAsync();
            return "Coupon Added";
        }

        public async Task<string> DeleteCoupon(Coupon coupon)
        {
           _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return "Coupon deleted";
        }

        public async  Task<List<Coupon>> GetAllCoupons()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetCoupon(string code)
        {
            return await _context.Coupons.Where(x=>x.CouponCode==code).FirstOrDefaultAsync();
        }

        public async Task<Coupon> GetCoupons(Guid Id)
        {
            return await _context.Coupons.Where(x=>x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateCoupon(Coupon Updatedcoupon)
        {
            await _context.SaveChangesAsync();
            return "Coupon Updated";
        }
    }
}
