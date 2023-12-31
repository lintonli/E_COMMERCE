using AutoMapper;
using CARTSERVICE.Models;
using CARTSERVICE.Models.Dtos;

namespace CARTSERVICE.Profiles
{
    public class CartProfiles:Profile
    {
        public CartProfiles()
        {
            CreateMap<CartItemsDto, CartItems>().ReverseMap();
            CreateMap<AddtoCartDto, Cart>().ReverseMap();
            CreateMap<AddtoCartDto, CartItems>().ReverseMap();
            CreateMap<CartResponseDto, Cart>().ReverseMap();
        }
    }
}
