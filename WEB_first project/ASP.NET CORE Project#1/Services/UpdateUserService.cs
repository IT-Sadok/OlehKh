using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ASP.NET_CORE_Project_1.Services
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateUserAsync(Guid userId, UpdateUserModel model, string currentUserId, bool isAdmin)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return (false, "User not found");
            }

            if (currentUserId != user.Id.ToString() && !isAdmin)
            {
                return (false, "You are not authorized to update this user.");
            }

            if (!string.IsNullOrEmpty(model.NewPhoneNumber))
            {
                user.PhoneNumber = model.NewPhoneNumber;
            }

            if (!string.IsNullOrEmpty(model.NewEmail))
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.NewEmail);
                if (!setEmailResult.Succeeded)
                {
                    return (false, "Failed to update email.");
                }
            }

            if (!string.IsNullOrEmpty(model.NewUserName))
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.NewUserName);
                if (!setUserNameResult.Succeeded)
                {
                    return (false, "Failed to update username.");
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return (false, "Failed to update user information.");
            }

            return (true, null);
        }

    }
}
