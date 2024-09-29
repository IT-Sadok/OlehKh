using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE_Project_1.Services
{
    public class ChangeDriverService : IChangeDriverService
    {
        private readonly ApplicationContext _context;

        public ChangeDriverService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeDriverAsync(int orderId, Guid newDriverId)
        {
            var order = await _context.Orders.Include(o => o.Driver).FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }

            if (newDriverId == Guid.Empty)
            {
                if (order.Status != "Assigned")
                {
                    return false;
                }

                order.DriverId = null;
                order.Status = "Pending";
            }
            else
            {
                if (order.Status != "Pending")
                {
                    return false;
                }

                order.DriverId = newDriverId;
                order.Status = "Assigned";
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
