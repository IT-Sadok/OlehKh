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

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        // Тільки пасажир може створювати замовлення
        [Authorize(Roles = "Passenger")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var order = await _orderService.CreateOrderAsync(user.Id, model.PickupLocation, model.Destination);
            return Ok(order);
        }

        // Тільки водій або адміністратор можуть переглядати всі замовлення
        [Authorize(Roles = "Admin,Driver")]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        // Призначити водія на замовлення (водій або адміністратор)
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

        // Завершити замовлення (водій або адміністратор)
        [Authorize(Roles = "Admin,Driver")]
        [HttpPut("{orderId}/complete")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var result = await _orderService.CompleteOrderAsync(orderId);
            if (!result) return BadRequest("Failed to complete order");
            return Ok();
        }
    }
}
