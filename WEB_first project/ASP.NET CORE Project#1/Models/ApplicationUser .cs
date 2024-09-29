using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ASP.NET_CORE_Project_1.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public string? PrimaryRole { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Order> PassengerOrders { get; set; }

        public virtual ICollection<Order> DriverOrders { get; set; }
    }
}
