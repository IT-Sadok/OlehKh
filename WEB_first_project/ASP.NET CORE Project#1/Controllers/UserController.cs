using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ASP.NET_CORE_Project_1.Commands.Users;
using System;
using System.Threading.Tasks;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;



        public UserController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("Current user not found.");
            }

            var isAdmin = User.IsInRole("Admin");

            var result = await _mediator.Send(new UpdateUserCommand(userId, model, currentUser.Id.ToString(), isAdmin));

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("User information updated successfully.");
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId.ToString());
            if (userToDelete == null)
            {
                return NotFound("User not found");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var isAdmin = User.IsInRole("Admin");

            if (currentUser.Id != userId && !isAdmin)
            {
                return Forbid("You are not allowed to delete other users.");
            }

            var result = await _mediator.Send(new DeleteUserCommand(userToDelete.Id, currentUser.Id.ToString(), isAdmin));

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok($"User {userToDelete.UserName} deleted successfully.");
        }
    }
}
