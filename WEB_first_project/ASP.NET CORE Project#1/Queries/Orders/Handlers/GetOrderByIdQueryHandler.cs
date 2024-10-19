using MediatR;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.DTO;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE_Project_1.Queries.Orders.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO?>
    {
        private readonly ApplicationContext _context;

        public GetOrderByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderDTO?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Passenger)
                .Include(o => o.Driver)
                .FirstOrDefaultAsync(o => o.Id == request.Id);

            if (order == null)
            {
                return null;
            }

            return new OrderDTO
            {
                Id = order.Id,
                PassengerId = order.PassengerId,
                DriverId = order.DriverId,
                PickupLocation = order.PickupLocation,
                Destination = order.Destination,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            };
        }
    }
}
