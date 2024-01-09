using AutoMapper;
using Azure;
using CARTSERVICE.Data;
using CARTSERVICE.Models;
using CARTSERVICE.Models.Dtos;
using CARTSERVICE.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CARTSERVICE.Services
{
    public class CartServices : ICart
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<string> AddToCart(CartItems cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);    
            await _context.SaveChangesAsync();
            return "Product Added to cart";
            /*  _context.CartItems.Add(cartItems);
              await _context.SaveChangesAsync();
              return "Product added to cart";*/

        }

        public async Task<string> CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return "Cart Created";
        }

        public async Task<string> DeleteCart(Guid CartId)
        {
            var cart = await _context.Carts.Where(x => x.Id == CartId).FirstOrDefaultAsync();
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return "Cart Deleted";
            }
            return "Cart does not exist";
        }

        public async Task<string> DeleteProductFromCart(Guid ProductId)
        {
            var item = await _context.CartItems.Where(x => x.ProductId == ProductId).FirstOrDefaultAsync();
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
                return "Product Removed From Cart";
            }
            return "Product Does not Exist";
        }

        public async Task<Cart> GetCartById(Guid CartId)
        {
            var cartid = await _context.Carts.Where(x => x.Id == CartId).
                 Include(cartid => cartid.CartItems).Select(cartid => new Cart()
                 {
                     Id = CartId,
                     UserId = cartid.UserId,
                     CartItems = _mapper.Map<List<CartItems>>(cartid.CartItems.ToList()),
                     TotalAmount = cartid.TotalAmount,
                 }).
                FirstOrDefaultAsync();
            return cartid;
        }

        public async Task<CartResponseDto> GetCartUserById(Guid UserId)
        {
            var cart = await _context.Carts.Where(x => x.UserId == UserId).
                Include(cart => cart.CartItems).Select(cart => new CartResponseDto()
                {
                    CartId = cart.Id,
                    UserId = UserId,
                    CartItems = _mapper.Map<List<CartItemsDto>>(cart.CartItems.ToList()),
                    TotalAmount = cart.TotalAmount,
                }).FirstOrDefaultAsync();
            return cart;
        }

        public async Task UpdateCartTotal(Guid UserId, decimal total)
        {
            var cart = await _context.Carts.Where(cart => cart.UserId == UserId).FirstOrDefaultAsync();
            cart.TotalAmount += total;
            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateProductQuantity(Guid CartId, int quantity)
        {
            var prod = await _context.CartItems.Where(x => x.CartId == CartId).FirstOrDefaultAsync();
            prod.ProductQuantity = quantity;
            await _context.SaveChangesAsync();
            return "Product Quantity Updated";

        }
    }
}
