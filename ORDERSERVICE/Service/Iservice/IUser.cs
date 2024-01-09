using ORDERSERVICE.Models.Dtos;

namespace ORDERSERVICE.Service.Iservice
{
    public interface IUser
    {
        Task<UserDto>GetUserById(Guid Id);
    }
}
