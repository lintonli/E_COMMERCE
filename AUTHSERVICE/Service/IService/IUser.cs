using AUTHSERVICE.Models.Dtos;

namespace AUTHSERVICE.Service.IService
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterUserDto userDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignUserRoles(string Email, string RoleName);
    }
}
