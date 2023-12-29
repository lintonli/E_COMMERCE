using AUTHSERVICE.Models;
using AUTHSERVICE.Models.Dtos;
using AutoMapper;

namespace AUTHSERVICE.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest=>dest.UserName, src=>src.MapFrom(r=>r.Email));
            CreateMap<UserDto, ApplicationUser>().ReverseMap(); 
        }
    }
}
