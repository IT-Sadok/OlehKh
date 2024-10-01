using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ASP.NET_CORE_Project_1.Services
{
    public class RemoveDriverFromOrderService : IRemoveDriverFromOrderService
    {
        private readonly IOrderService _orderService;
        private readonly IChangeDriverService _changeDriverService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RemoveDriverFromOrderService(IOrderService orderService, IChangeDriverService changeDriverService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _changeDriverService = changeDriverService;
            _userManager = userManager;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RemoveDriverAsync(int orderId, string userId, string role)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return (false, $"Order with ID {orderId} not found.");
            }

            if (order.Status != "Assigned")
            {
                return (false, "Driver can only be removed if the order status is 'Assigned'.");
            }

            if (role == "Driver" && order.DriverId != Guid.Parse(userId))
            {
                return (false, "You are not assigned to this order.");
            }
            else if (role != "Admin" && role != "Driver")
            {
                return (false, "Unauthorized.");
            }

            var removeResult = await _changeDriverService.ChangeDriverAsync(orderId, Guid.Empty);
            if (!removeResult)
            {
                return (false, "Failed to remove driver from the order.");
            }

            return (true, "Driver removed successfully.");
        }


    }
}
