using MediatR;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Services;

namespace ASP.NET_CORE_Project_1.Commands.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return (false, "User not found.");
            }

            if (currentUserId != user.Id.ToString() && !request.IsAdmin)
            {
                return (false, "You are not authorized to delete this user.");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return (false, "Failed to delete user.");
            }

            return (true, "User deleted successfully.");
        }
    }
}
