using Microsoft.AspNetCore.Identity;
using System;

namespace ASP.NET_CORE_Project_1.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? PrimaryRole { get; set; }
    }
}
