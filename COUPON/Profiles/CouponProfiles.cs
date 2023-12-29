using AutoMapper;
using COUPON.Models;
using COUPON.Models.Dtos;

namespace COUPON.Profiles
{
    public class CouponProfiles:Profile
    {
        public CouponProfiles()
        {
            CreateMap<AddCouponDto, Coupon>().ReverseMap();
        }
    }
}
