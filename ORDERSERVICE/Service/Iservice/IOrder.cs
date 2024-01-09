using ORDERSERVICE.Models;
using ORDERSERVICE.Models.Dtos;

namespace ORDERSERVICE.Service.Iservice
{
    public interface IOrder
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(Guid Id);
        Task<string> AddOrder(Order order);
        Task<string> UpdateOrder(Order order);
        Task<string> DeleteOrder(Order ord);
        Task<List<Order>> GetOrderByUserId(Guid UserId);
        Task saveChanges();
        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);
        Task<bool> ValidatePayments(Guid OrderId);
    }
}
