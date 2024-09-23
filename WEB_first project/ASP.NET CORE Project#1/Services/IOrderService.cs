using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Guid passengerId, string pickupLocation, string destination);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<bool> AssignDriverAsync(int orderId, Guid driverId);
        Task<bool> CompleteOrderAsync(int orderId);
    }
}
