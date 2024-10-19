using MediatR;
using ASP.NET_CORE_Project_1.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE_Project_1.Commands.Orders.Driver.Handlers
{
    public class ChangeDriverCommandHandler : IRequestHandler<ChangeDriverCommand, bool>
    {
        private readonly ApplicationContext _context;

        public ChangeDriverCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ChangeDriverCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(o => o.Driver).FirstOrDefaultAsync(o => o.Id == request.OrderId);
            if (order == null)
            {
                return false;
            }

            order.DriverId = request.NewDriverId;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
