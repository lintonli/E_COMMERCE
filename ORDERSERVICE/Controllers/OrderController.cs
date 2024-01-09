using AutoMapper;
using Azure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORDERSERVICE.Models;
using ORDERSERVICE.Models.Dtos;
using ORDERSERVICE.Service.Iservice;
using System.Security.Claims;
using ResponseDto = ORDERSERVICE.Models.Dtos.ResponseDto;

namespace ORDERSERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrder _orderServices;
        private readonly ICoupon _couponServices;
        private readonly ICart _cartServices;
  
        private readonly ResponseDto _responseDto;
        public OrderController(IMapper mapper, IOrder orderservices, ICart cartServices,ICoupon coupon)
        {
            _mapper = mapper;
            _orderServices = orderservices;
            _cartServices = cartServices;
            _couponServices = coupon;
            _responseDto = new ResponseDto();
         
            _cartServices = cartServices;
           
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddOrder(AddOrderDto orderDto)
        {
            /*var order=await _orderServices.GetOrderById(cartDto.Id);
            if (order == null) {
                return BadRequest("Order Id not found");

            } return Ok(order);*/
            var order= _mapper.Map<Order>(orderDto);
            var cart = await _cartServices.GetCartById(order.CartId);
            if(cart == null)
            {
                _responseDto.Errormessage = "Cart does not exist";
                return NotFound(_responseDto);
            }
            order.CartId = cart.Id;
            order.TotalAmount = cart.TotalAmount;
            order.UserId = cart.UserId;
            
           var res= await _orderServices.AddOrder(order);
            _responseDto.Result= res;
            return Ok(res); 
        }
        [HttpPost("Pay")]
        public async Task<ActionResult<ResponseDto>>MakePayments(StripeRequestDto stripeRequest)
        {
            var striperequest = await _orderServices.MakePayments(stripeRequest);
            _responseDto.Result = striperequest;
            return Ok(_responseDto);
        }
        [HttpPost("validate/{Id}")]

        public async Task<ActionResult<ResponseDto>> validatePayment(Guid Id)
        {
       /*     var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];*/
            var res = await _orderServices.ValidatePayments(Id);

            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }

            _responseDto.Errormessage = "Payment Failed!";
            return BadRequest(_responseDto);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseDto>>GetUserOrders(Guid UserId)
        {
            var response = await _orderServices.GetOrderByUserId(UserId);
            _responseDto.Result = response;
            return Ok(_responseDto);

        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAll()
        {
            var response = await _orderServices.GetOrders();
            _responseDto.Result = response;
            return Ok(_responseDto);
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ResponseDto>>ApplyCoupon(Guid Id, string Code)
        {
            var order = await _orderServices.GetOrderById(Id);
            if (order == null)
            {
                _responseDto.Errormessage = "Order not found";
                return NotFound(_responseDto);
            }
            var coupon = await _couponServices.GetCouponByCouponCode(Code);
            if (coupon == null)
            {
                _responseDto.Errormessage = "Coupon code is invalid";
                return NotFound(_responseDto);
            }
            if (coupon.CouponMinAmount <= order.TotalAmount)
            {
                order.CouponCode = coupon.CouponCode;
                order.Discount = coupon.CouponAmount;
                await _orderServices.saveChanges();
                _responseDto.Result = "Code has been Applied";
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.Errormessage = "Total Amount is less then Minimum Amount for the Coupon";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
