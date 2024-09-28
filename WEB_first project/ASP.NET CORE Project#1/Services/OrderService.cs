using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.DTO;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE_Project_1.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;

        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderDTO> CreateOrderAsync(Guid passengerId, string pickupLocation, string destination)
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

            return new OrderDTO
            {
                Id = order.Id,
                PassengerId = order.PassengerId,
                PickupLocation = order.PickupLocation,
                Destination = order.Destination,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            };
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            var orders = await _context.Orders.Include(o => o.Passenger).Include(o => o.Driver).ToListAsync();

            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                PassengerId = order.PassengerId,
                PickupLocation = order.PickupLocation,
                Destination = order.Destination,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            });
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.Include(o => o.Passenger).Include(o => o.Driver).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return null;
            }

            return new OrderDTO
            {
                Id = order.Id,
                PassengerId = order.PassengerId,
                PickupLocation = order.PickupLocation,
                Destination = order.Destination,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            };
        }

        public async Task<bool> AssignDriverAsync(int orderId, Guid driverId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null || order.DriverId != null) return false;

            var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            orderEntity.DriverId = driverId;
            orderEntity.Status = "Assigned";
            orderEntity.AssignedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null || order.Status != "Assigned") return false;

            var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            orderEntity.Status = "Completed";
            orderEntity.CompletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
