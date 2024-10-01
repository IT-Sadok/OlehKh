using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Services;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IChangeDriverService _changeDriverService;
        private readonly IRemoveDriverFromOrderService _removeDriverFromOrderService;

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager,
            IChangeDriverService changeDriverService, IRemoveDriverFromOrderService removeDriverFromOrderService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _changeDriverService = changeDriverService;
            _removeDriverFromOrderService = removeDriverFromOrderService;
        }

        [Authorize(Roles = "Passenger")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var order = await _orderService.CreateOrderAsync(user.Id, model.PickupLocation, model.Destination);
            return Ok(order);
        }

        [Authorize(Roles = "Admin,Driver")]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [Authorize(Roles = "Admin,Driver")]
        [HttpPut("{orderId}/assign")]
        public async Task<IActionResult> AssignDriver(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !User.IsInRole("Driver")) return Unauthorized();

            var result = await _orderService.AssignDriverAsync(orderId, user.Id);
            if (!result) return BadRequest("Failed to assign driver");
            return Ok();
        }

        [Authorize(Roles = "Admin,Driver")]
        [HttpPut("{orderId}/complete")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var result = await _orderService.CompleteOrderAsync(orderId);
            if (!result) return BadRequest("Failed to complete order");
            return Ok();
        }

        [Authorize(Roles = "Driver,Admin")]
        [HttpDelete("{orderId}/Driver")]
        public async Task<IActionResult> RemoveDriver(int orderId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            var userId = currentUser.Id.ToString();

            var result = await _removeDriverFromOrderService.RemoveDriverAsync(orderId, userId, User.IsInRole("Driver") ? "Driver" : "Admin");

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("Driver removed from the order successfully.");
        }
    }
}
