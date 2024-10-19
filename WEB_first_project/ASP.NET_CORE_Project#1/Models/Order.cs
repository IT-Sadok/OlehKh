using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE_Project_1.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Passenger")]
        public Guid PassengerId { get; set; }

        public ApplicationUser? Passenger { get; set; }

        [ForeignKey("Driver")]
        public Guid? DriverId { get; set; }

        public ApplicationUser? Driver { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string PickupLocation { get; set; } = string.Empty;

        [Required]
        public string Destination { get; set; } = string.Empty;

        [Required]
        public EnumOrderStatus Status { get; set; }

        public DateTime? AssignedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
