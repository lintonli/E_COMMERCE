using CARTSERVICE.Models;
using CARTSERVICE.Models.Dtos;

namespace CARTSERVICE.Services.IServices
{
    public interface ICart
    {
        Task<string> CreateCart(Cart cart);
        Task<Cart> GetCartById(Guid CartId);
        Task<CartResponseDto>GetCartUserById(Guid UserId);
        Task<string>AddToCart(CartItems cartItems);
        Task<string> DeleteCart(Guid CartId);
        Task<string>UpdateProductQuantity(Guid CartId, int quantity);

        Task UpdateCartTotal(Guid UserId, decimal total);
        Task <string> DeleteProductFromCart(Guid ProductId);
    }
}
