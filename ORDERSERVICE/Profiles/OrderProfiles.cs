using AutoMapper;
using ORDERSERVICE.Models;
using ORDERSERVICE.Models.Dtos;

namespace ORDERSERVICE.Profiles
{
    public class OrderProfiles:Profile
    {
        public OrderProfiles()
        {
            CreateMap<AddOrderDto, Order>().ReverseMap();
        }
    }
}
