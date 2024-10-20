using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public Guid PassengerId { get; set; }
        public Guid? DriverId { get; set; }
        public string? PickupLocation { get; set; }
        public string? Destination { get; set; }
        public EnumOrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
