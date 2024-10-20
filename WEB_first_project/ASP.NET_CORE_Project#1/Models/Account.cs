using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE_Project_1.Models
{
    [Table(nameof(Account))]
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Role { get; set; }

        public int? DrivingExperienceYears { get; set; }

        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? CarModel { get; set; }
    }
}
