using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PRODUCTSERVICE.Models;
using PRODUCTSERVICE.Models.Dtos;
using PRODUCTSERVICE.Service.IService;

namespace PRODUCTSERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productServices;
       private readonly IMapper _mapper;
        private readonly ResponseDto responseDto;
        public ProductController(IProduct productServices,IMapper mapper)
        {
            _productServices = productServices;
            _mapper = mapper;
            responseDto = new ResponseDto();    
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddProduct(AddProductDto dto)
        {
            var prod = _mapper.Map<Product>(dto);
            var res = await _productServices.AddProduct(prod);
            responseDto.Result = res;
            return Created("", responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllProducts()
        {

            var res = await _productServices.GetProducts();
            responseDto.Result = res;
            return Ok(responseDto);
        }
        [HttpDelete("{Id}")]
        public async Task<string> DeleteProduct(Guid Id)
        {
            var prod = await _productServices.GetProductById(Id);
            if (prod == null)
            {
                return "Product not found";
            }
            var res = await _productServices.DeleteProduct(prod);
            return "Product Deleted Successfully";
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetProduct(Guid Id)
        {
            var post = await _productServices.GetProductById(Id);
            if (post == null)
            {
                return NotFound(responseDto);
            }
            responseDto.Result = post;
            return Ok(responseDto);
        }
    }
}
