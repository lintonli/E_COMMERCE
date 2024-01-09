using AutoMapper;
using Azure;
using COUPON.Models;
using COUPON.Models.Dtos;
using COUPON.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

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
        [HttpGet("single/{Id}")]
        [Authorize]
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
        [HttpGet("{Code}")]

        public async Task<ActionResult<ResponseDto>> GetCoupon(string Code)
        {
            var coupon = await _couponService.GetCoupon(Code);
            if (coupon == null)
            {
                _responseDto.ErrorMessage = "Coupon Not found";
                return NotFound(_responseDto);
            }
            _responseDto.Result = coupon;
            return Ok(_responseDto);
        }
        [HttpPost]
        [Authorize]
         public async Task<ActionResult<ResponseDto>>AddCoupon(AddCouponDto addCoupon)
        {
            var coupon = _mapper.Map<Models.Coupon>(addCoupon);
            var response = await _couponService.AddCoupon(coupon);
            _responseDto.Result = response;
            var options = new CouponCreateOptions()
            {
                AmountOff = (long)addCoupon.CouponAmount * 100,
                Currency = "kes",
                Id = addCoupon.CouponCode,
                Name = addCoupon.CouponCode
            };

            var service = new Stripe.CouponService();
            service.Create(options);

            return Created("", _responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCoupons();
            _responseDto.Result = coupons;
            return Ok(_responseDto);
        }

        [HttpDelete("{Id}")]
        [Authorize]
  
        public async Task<ActionResult<ResponseDto>> deleteCoupon(Guid Id)
        {
            var coupon = await _couponService.GetCoupons(Id);
            if (coupon == null)
            {
                _responseDto.Result = "Coupon Not Found";
                _responseDto.IsSuccess = false;
                return NotFound(_responseDto);
            }
            var res = await _couponService.DeleteCoupon(coupon);

            var service = new Stripe.CouponService();
            service.Delete(coupon.CouponCode);

            _responseDto.Result = res;
            return Ok(_responseDto);
        }
        [HttpPut("{Id}")]
        [Authorize]
 
        public async Task<ActionResult<ResponseDto>> updateCoupon(Guid Id, AddCouponDto UCoupon)
        {
            var coupon = await _couponService.GetCoupons(Id);
            if (coupon == null)
            {
                _responseDto.Result = "Coupon Not Found";
                _responseDto.IsSuccess = false;
                return NotFound(_responseDto);
            }
            _mapper.Map(UCoupon, coupon);
            var res = await _couponService.UpdateCoupon(coupon);
            var service = new Stripe.CouponService();
            service.Delete(coupon.CouponCode);

            var options = new CouponCreateOptions()
            {
                AmountOff = (long)UCoupon.CouponAmount * 100,
                Currency = "kes",
                Id = UCoupon.CouponCode,
                Name = UCoupon.CouponCode
            };

            service.Create(options);


            _responseDto.Result = res;
            return Ok(_responseDto);
        }
    }
}
