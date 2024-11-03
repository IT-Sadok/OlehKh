using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MediatR;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Commands.Orders;
using ASP.NET_CORE_Project_1.Queries.Orders;
using ASP.NET_CORE_Project_1.Commands.Orders.Driver;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize(Roles = "Passenger")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var order = await _mediator.Send(new CreateOrderCommand(user.Id, model.PickupLocation, model.Destination));

            return Ok(order);
        }

        [Authorize(Roles = "Admin,Driver")]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return Ok(orders);
        }

        [Authorize(Roles = "Admin,Driver")]
        [HttpPut("{orderId}/assign")]
        public async Task<IActionResult> AssignDriver(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !User.IsInRole("Driver"))
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(new AssignDriverCommand(orderId, user.Id));

            if (result)
            {
                return Ok();
            }

            return BadRequest("Failed to assign driver");
        }

        [Authorize(Roles = "Admin,Driver")]
        [HttpPut("{orderId}/complete")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var result = await _mediator.Send(new CompleteOrderCommand(orderId));

            if (result)
            {
                return Ok();
            }

            return BadRequest("Failed to complete order");
        }

        [Authorize(Roles = "Driver,Admin")]
        [HttpDelete("{orderId}/driver")]
        public async Task<IActionResult> RemoveDriver(int orderId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            var userId = currentUser.Id.ToString();
            var role = User.IsInRole("Driver") ? "Driver" : "Admin";

            var result = await _mediator.Send(new RemoveDriverCommand(orderId, userId, role));

            return result.IsSuccess ? Ok("Driver removed from the order successfully.") : BadRequest(result.ErrorMessage);
        }
    }
}
