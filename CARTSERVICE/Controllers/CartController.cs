﻿using AutoMapper;
using CARTSERVICE.Models;
using CARTSERVICE.Models.Dtos;
using CARTSERVICE.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CARTSERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cartService;
        private readonly IProduct _productService;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        public CartController(ICart cartService, IMapper mapper, IProduct productService)
        {
            _cartService = cartService;
            _mapper = mapper;
            _productService = productService;
            _response = new ResponseDto();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddProducttoCart(AddtoCartDto cartDto)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            var cart = await _cartService.GetCartUserById(new Guid(userId));

            
            if (cart == null) 
            {
                await _cartService.CreateCart(new Cart() { UserId = new Guid(userId)});
                cart = await _cartService.GetCartUserById(new Guid(userId));
            }
            var product = await _productService.GetProductById(cartDto.ProductId,token);
            if (product == null)
            {
                _response.ErrorMessage = "Product not Found";
                return NotFound(_response);
            }

            if (cartDto.ProductQuantity == 0)
            {
                _response.ErrorMessage = "Kindly add atleast one product";
                return BadRequest(_response);
            }

            var cartItem = new CartItems()
            {

                ProductId = product.Id,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImage = product.Image,
                ProductPrice = product.Price,
                ProductQuantity = cartDto.ProductQuantity,
                CartId = cart.CartId,

            };
            
            var res = await _cartService.AddToCart(cartItem);
            await _cartService.UpdateCartTotal(new Guid(userId), (cartItem.ProductPrice * cartItem.ProductQuantity));
            _response.Result = res;
            return Ok(res);
        }
        [HttpGet("{ProductId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> getProducts(Guid ProductId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                var res = await _productService.GetProductById(ProductId,token);
                if (res == null)
                {
                    _response.ErrorMessage = "Product does not exist";
                    return NotFound(_response);
                }
                _response.Result = res;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return (_response);
            }
        }
        [HttpDelete("{ProductId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> RemoveItem(Guid ProductId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var prod = await _cartService.DeleteProductFromCart(ProductId);
            if (prod == null)
            {
                _response.ErrorMessage = "The product you are trying to does not exist";
                return NotFound(_response);
            }
            var product = await _productService.GetProductById(ProductId, token);
            _response.Result = $"{product.Name} has been removed from cart";
            return Ok(_response);
        }
        [HttpGet("single/{CartId}")]
        public async Task<ActionResult<ResponseDto>>Getcart(Guid CartId)
        {
            var crt = await _cartService.GetCartById(CartId);
            if(crt == null)
            {
                _response.ErrorMessage = "Cart does not exist";
                return NotFound(_response);
            }
            _response.Result = crt;
            return Ok(_response);
        }

    }
}
