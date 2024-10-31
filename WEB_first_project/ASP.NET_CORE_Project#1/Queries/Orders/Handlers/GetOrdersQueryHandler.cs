using MediatR;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.DTO;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE_Project_1.Queries.Orders.Handlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly ApplicationContext _context;

        public GetOrdersQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(o => o.Passenger)
                .Include(o => o.Driver)
                .ToListAsync();

            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                PassengerId = order.PassengerId,
                DriverId = order.DriverId,
                PickupLocation = order.PickupLocation,
                Destination = order.Destination,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            });
        }
    }
}
