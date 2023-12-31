using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRODUCTSERVICE.Data;
using PRODUCTSERVICE.Models;
using PRODUCTSERVICE.Models.Dtos;
using PRODUCTSERVICE.Service.IService;

namespace PRODUCTSERVICE.Service
{
    public class ProductServices : IProduct
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> AddProduct(Product product)
        {
           _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return "Product Added";
        }

        public async Task<string> DeleteProduct(Product product)
        {
           _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return "Product Deleted";
        }

        public async Task<Product> GetProductById(Guid productId)
        {
           return await _context.Products.Where(x=>x.Id==productId).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            var prod= await _context.Products.ToListAsync();
        /*    var res= _mapper.Map<List<AddProductDto>>(prod);*/
            return prod;
        }

        public async Task<string> UpdateProduct(Product product)
        {
            await _context.SaveChangesAsync();
            return "Product Updated";
        }
    }
}
