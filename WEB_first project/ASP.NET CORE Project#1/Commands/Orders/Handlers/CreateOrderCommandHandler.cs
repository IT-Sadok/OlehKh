using MediatR;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Commands.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly ApplicationContext _context;

        public CreateOrderCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                PassengerId = request.PassengerId,
                PickupLocation = request.PickupLocation,
                Destination = request.Destination,
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
    }
}
