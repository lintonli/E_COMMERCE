using PRODUCTSERVICE.Models;
using PRODUCTSERVICE.Models.Dtos;

namespace PRODUCTSERVICE.Service.IService
{
    public interface IProduct
    {
        Task<List<AddProductDto>> GetProducts();
        Task<string> AddProduct(Product product);
        Task<Product> GetProductById(Guid productId);
        Task<string>DeleteProduct(Product product);
        Task<string>UpdateProduct(Product product);
    }
}
