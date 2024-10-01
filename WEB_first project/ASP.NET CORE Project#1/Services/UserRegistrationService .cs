using ASP.NET_CORE_Project_1.Constants;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP.NET_CORE_Project_1.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRegistrationService _registrationService;

        public UserRegistrationService(UserManager<ApplicationUser> userManager, IRegistrationService registrationService)
        {
            _userManager = userManager;
            _registrationService = registrationService;
        }

        public async Task<IActionResult> RegisterUserAsync(BaseSignUpModel model, string role, ClaimsPrincipal user)
        {
            if (string.IsNullOrEmpty(role))
            {
                return new BadRequestObjectResult("Role is required");
            }

            var anyAdminsExist = await _userManager.GetUsersInRoleAsync(UserRoles.Admin);

            if (role.ToLower() == UserRoles.Admin.ToLower())
            {
                if (anyAdminsExist.Any() && !user.IsInRole(UserRoles.Admin))
                {
                    return new ForbidResult("Only existing admins can create new admins.");
                }
            }

            return await RegisterWithRoleAsync(model, role);
        }

        private async Task<IActionResult> RegisterWithRoleAsync(BaseSignUpModel model, string role)
        {
            var result = await _registrationService.RegisterUserAsync(model, role);

            if (result.IsSuccess)
            {
                return new OkObjectResult("User registered successfully.");
            }

            return new BadRequestObjectResult("User registration failed.");
        }
    }
}
