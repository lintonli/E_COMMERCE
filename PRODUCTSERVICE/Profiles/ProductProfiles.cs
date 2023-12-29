using AutoMapper;
using PRODUCTSERVICE.Models;
using PRODUCTSERVICE.Models.Dtos;

namespace PRODUCTSERVICE.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles()
        {
            CreateMap<AddProductDto, Product>().ReverseMap();
        }
    }
}
