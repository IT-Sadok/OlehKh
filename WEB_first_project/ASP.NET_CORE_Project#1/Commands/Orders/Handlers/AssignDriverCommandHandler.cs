using MediatR;
using ASP.NET_CORE_Project_1.Data;
using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Commands.Orders.Handlers
{
    public class AssignDriverCommandHandler : IRequestHandler<AssignDriverCommand, bool>
    {
        private readonly ApplicationContext _context;

        public AssignDriverCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AssignDriverCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId);

            if (order == null)
            {
                return false;
            }

            if (order.DriverId != null)
            {
                return false;
            }

            order.DriverId = request.DriverId;
            order.Status = EnumOrderStatus.Assigned;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
