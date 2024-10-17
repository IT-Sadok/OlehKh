using Microsoft.EntityFrameworkCore;
using Xunit;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ASP.NET_CORE_Project_1.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Data;
using System.Security.Claims;
using MediatR;
using ASP.NET_CORE_Project_1.Commands.Orders;


namespace TestProject1
{
    public class OrdersControllerTests
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OrdersController _controller;
        private readonly ApplicationContext _context;

        public OrdersControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationContext(options);

            _mediator = Substitute.For<IMediator>();
            _userManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            _controller = new OrdersController(_mediator, _userManager);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "DriverUser"),
            new Claim(ClaimTypes.Role, "Driver")
            }, "TestAuthentication"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            SeedTestData();
        }

        private void SeedTestData()
        {
            _context.Orders.Add(new Order { Id = 1, Status = "Pending" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task AssignDriver_UserIsDriver_AssignsDriverSuccessfully()
        {
            // Arrange
            var user = new ApplicationUser { Id = Guid.NewGuid() };
            _userManager.GetUserAsync(Arg.Any<ClaimsPrincipal>()).Returns(Task.FromResult(user));
            _mediator.Send(Arg.Any<AssignDriverCommand>()).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.AssignDriver(1);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task AssignDriver_UserNotDriver_ReturnsUnauthorized()
        {
            // Arrange
            var user = new ApplicationUser { Id = Guid.NewGuid() };

            _userManager.GetUserAsync(Arg.Any<ClaimsPrincipal>()).Returns(Task.FromResult(user));

            _userManager.IsInRoleAsync(user, "Driver").Returns(Task.FromResult(false));
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Passenger")
            }));

            // Act
            var result = await _controller.AssignDriver(1);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}