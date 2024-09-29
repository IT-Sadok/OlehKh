using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
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

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPatch("{userId}/updateUser")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            // Якщо користувач не є адміністратором, він може оновлювати тільки свої дані
            if (currentUser.Id != userId && !User.IsInRole("Admin"))
            {
                return Forbid(); // Забороняємо змінювати чужі дані для звичайних користувачів
            }

            // Оновлюємо тільки ті поля, які були передані
            if (!string.IsNullOrEmpty(model.NewPhoneNumber))
            {
                user.PhoneNumber = model.NewPhoneNumber;
            }

            if (!string.IsNullOrEmpty(model.NewEmail))
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.NewEmail);
                if (!setEmailResult.Succeeded)
                {
                    return BadRequest("Failed to update email.");
                }
            }

            if (!string.IsNullOrEmpty(model.NewUserName))
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.NewUserName);
                if (!setUserNameResult.Succeeded)
                {
                    return BadRequest("Failed to update username.");
                }
            }

            // Оновлюємо користувача в системі
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                return Ok("User information updated successfully.");
            }

            return BadRequest("Failed to update user information.");
        }


        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            // Отримуємо користувача, якого треба видалити
            var userToDelete = await _userManager.FindByIdAsync(userId.ToString());
            if (userToDelete == null)
            {
                return NotFound("User not found");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            // Перевіряємо, чи це адміністратор
            var isAdmin = User.IsInRole("Admin");

            // Якщо не адміністратор, користувач може видаляти тільки себе
            if (currentUser.Id != userId && !isAdmin)
            {
                return Forbid("You are not allowed to delete other users.");
            }

            // Видаляємо користувача
            var result = await _userManager.DeleteAsync(userToDelete);

            if (!result.Succeeded)
            {
                return BadRequest("Failed to delete user.");
            }

            return Ok($"User {userToDelete.UserName} deleted successfully.");
        }
    }
}
