using CARTSERVICE.Models.Dtos;

namespace CARTSERVICE.Services.IServices
{
    public interface IProduct
    {
        Task<ProductDto> GetProductById(Guid ProductId);
    }
}
