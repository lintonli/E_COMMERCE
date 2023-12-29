using AutoMapper;
using COUPON.Models;
using COUPON.Models.Dtos;
using COUPON.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COUPON.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICoupon _couponService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public CouponController(ICoupon coupon, IMapper mapper)
        {
            _couponService = coupon;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>>GetCoupon(Guid Id)
        {
            var coupon = await _couponService.GetCoupons(Id);
            if (coupon == null)
            {
                _responseDto.ErrorMessage = "Coupon Not Found";
                return NotFound(_responseDto);
            }
            _responseDto.Result = coupon;
            return Ok(_responseDto);
        }
        [HttpPost]
         public async Task<ActionResult<ResponseDto>>AddCoupon(AddCouponDto addCoupon)
        {
            var coupon = _mapper.Map<Coupon>(addCoupon);
            var response = await _couponService.AddCoupon(coupon);
            _responseDto.Result = response;
            return Created("", _responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCoupons();
            _responseDto.Result = coupons;
            return Ok(_responseDto);
        }
    }
}
