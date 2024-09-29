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

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager, IChangeDriverService changeDriverService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _changeDriverService = changeDriverService;
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
        [HttpPut("{orderId}/removeDriver")]
        public async Task<IActionResult> RemoveDriver(int orderId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Логування поточного користувача
            Console.WriteLine($"Current user: {currentUser?.UserName}, ID: {currentUser?.Id}");

            // Отримуємо замовлення з бази даних
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return NotFound($"Order with ID {orderId} not found.");
            }

            // Логування DriverId замовлення
            Console.WriteLine($"Order driver ID: {order.DriverId}");

            // Перевірка статусу замовлення
            if (order.Status != "Assigned")
            {
                return BadRequest("Driver can only be removed if the order status is 'Assigned'.");
            }

            // Якщо поточний користувач — водій
            if (User.IsInRole("Driver"))
            {
                // Перевіряємо, чи цей водій закріплений за цим замовленням за допомогою DriverId
                if (order.DriverId != currentUser?.Id)
                {
                    Console.WriteLine("You are not assigned to this order.");
                    return Unauthorized("You are not assigned to this order.");
                }
            }
            else if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }

            // Знімаємо водія з замовлення (якщо умови виконано)
            var removeResult = await _changeDriverService.ChangeDriverAsync(orderId, Guid.Empty);
            if (!removeResult)
            {
                return BadRequest("Failed to remove driver from the order.");
            }

            return Ok("Driver removed from the order successfully.");
        }



    }
}
