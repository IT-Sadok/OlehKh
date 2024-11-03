using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE_Project_1.DTO
{
    public class CreateOrderModel
    {
        [Required]
        public string? PickupLocation { get; set; }

        [Required]
        public string? Destination { get; set; }
    }
}
