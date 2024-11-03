using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface ICurrentUserService
    {
        string GetCurrentUserId();
        ApplicationUser GetCurrentUser();
    }
}
