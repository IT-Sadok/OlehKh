using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
