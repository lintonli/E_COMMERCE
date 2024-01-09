using ORDERSERVICE.Models.Dtos;

namespace ORDERSERVICE.Service.Iservice
{
    public interface ICart
    {
        Task<CartDto> GetCartById(Guid Id);
    }
}
