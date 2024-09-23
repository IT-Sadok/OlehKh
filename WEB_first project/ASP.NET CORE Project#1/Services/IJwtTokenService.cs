using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface IJwtTokenService
    {
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    }

}
