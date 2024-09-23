using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.EntityFrameworkCore;
using static ASP.NET_CORE_Project_1.Services.OrderService;

namespace ASP.NET_CORE_Project_1.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;

        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Guid passengerId, string pickupLocation, string destination)
        {
            var order = new Order
            {
                PassengerId = passengerId,
                PickupLocation = pickupLocation,
                Destination = destination,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(o => o.Passenger).Include(o => o.Driver).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.Passenger).Include(o => o.Driver).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> AssignDriverAsync(int orderId, Guid driverId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null || order.DriverId != null) return false;

            order.DriverId = driverId;
            order.Status = "Assigned";
            order.AssignedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null || order.Status != "Assigned") return false;

            order.Status = "Completed";
            order.CompletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
