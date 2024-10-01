using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUpdateUserService _updateUserService;

        public UserController(UserManager<ApplicationUser> userManager, IUpdateUserService updateUserService)
        {
            _userManager = userManager;
            _updateUserService = updateUserService;
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

            var result = await _updateUserService.UpdateUserAsync(userId, model, currentUser.Id.ToString(), isAdmin);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("User information updated successfully.");
        }


        [Authorize]
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

            var result = await _userManager.DeleteAsync(userToDelete);

            if (!result.Succeeded)
            {
                return BadRequest("Failed to delete user.");
            }

            return Ok($"User {userToDelete.UserName} deleted successfully.");
        }
    }
}
