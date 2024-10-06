using ASP.NET_CORE_Project_1.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface IJwtTokenService
    {
        Task<JwtSecurityToken> GenerateJwtTokenAsync(ApplicationUser user);
    }

}
