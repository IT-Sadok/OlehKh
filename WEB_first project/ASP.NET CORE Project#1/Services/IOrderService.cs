using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderAsync(Guid passengerId, string pickupLocation, string destination);
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<bool> AssignDriverAsync(int orderId, Guid driverId);
        Task<bool> CompleteOrderAsync(int orderId);
    }
}
