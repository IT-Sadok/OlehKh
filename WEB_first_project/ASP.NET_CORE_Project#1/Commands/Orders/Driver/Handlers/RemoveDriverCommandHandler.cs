using MediatR;
using ASP.NET_CORE_Project_1.Data;
using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Commands.Orders.Driver.Handlers
{
    public class RemoveDriverCommandHandler : IRequestHandler<RemoveDriverCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly ApplicationContext _context;

        public RemoveDriverCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(RemoveDriverCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(o => o.Driver).FirstOrDefaultAsync(o => o.Id == request.OrderId);
            if (order == null)
            {
                return (false, $"Order with ID {request.OrderId} not found.");
            }

            if (order.Status != EnumOrderStatus.Assigned)
            {
                return (false, "Driver can only be removed if the order status is 'Assigned'.");
            }

            if (request.Role == "Driver" && order.DriverId != Guid.Parse(request.UserId))
            {
                return (false, "You are not assigned to this order.");
            }

            order.DriverId = null;
            order.Status = EnumOrderStatus.Pending;

            await _context.SaveChangesAsync();
            return (true, "Driver removed successfully.");
        }
    }
}
