using MediatR;
using ASP.NET_CORE_Project_1.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE_Project_1.Commands.Orders.Handlers
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, bool>
    {
        private readonly ApplicationContext _context;

        public CompleteOrderCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId);
            if (order == null || order.Status != "Assigned")
            {
                return false;
            }

            order.Status = "Completed";
            order.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
