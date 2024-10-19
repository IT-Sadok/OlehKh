using MediatR;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Services;

namespace ASP.NET_CORE_Project_1.Commands.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return (false, "User not found");
            }

            if (currentUserId != user.Id.ToString() && !request.IsAdmin)
            {
                return (false, "You are not authorized to update this user.");
            }

            if (!string.IsNullOrEmpty(request.Model.NewPhoneNumber))
            {
                user.PhoneNumber = request.Model.NewPhoneNumber;
            }

            if (!string.IsNullOrEmpty(request.Model.NewEmail))
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, request.Model.NewEmail);
                if (!setEmailResult.Succeeded)
                {
                    return (false, "Failed to update email.");
                }
            }

            if (!string.IsNullOrEmpty(request.Model.NewUserName))
            {
                var existingUser = await _userManager.FindByNameAsync(request.Model.NewUserName);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    return (false, "Username already exists.");
                }

                var setUserNameResult = await _userManager.SetUserNameAsync(user, request.Model.NewUserName);
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
