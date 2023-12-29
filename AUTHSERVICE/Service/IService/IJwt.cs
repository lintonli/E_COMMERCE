using AUTHSERVICE.Models;

namespace AUTHSERVICE.Service.IService
{
    public interface IJwt
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> Roles);
    }
}
