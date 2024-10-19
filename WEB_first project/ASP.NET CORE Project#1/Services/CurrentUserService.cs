using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ASP.NET_CORE_Project_1.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public ApplicationUser GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            return userId != null ? _userManager.FindByIdAsync(userId).Result : null;
        }
    }
}
