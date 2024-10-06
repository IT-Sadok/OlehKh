using MediatR;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Constants;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.Commands.Auth;

namespace ASP.NET_CORE_Project_1.Commands.Auth.Handlers
{
    public class RegisterUserWithRoleCommandHandler : IRequestHandler<RegisterUserWithRoleCommand, IActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;

        public RegisterUserWithRoleCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IActionResult> Handle(RegisterUserWithRoleCommand request, CancellationToken cancellationToken)
        {
            var anyAdminsExist = await _userManager.GetUsersInRoleAsync(UserRoles.Admin);

            if (request.Role.ToLower() == UserRoles.Admin.ToLower() && anyAdminsExist.Any() && !request.CurrentUser.IsInRole(UserRoles.Admin))
            {
                return new ForbidResult("Only existing admins can create new admins.");
            }

            var result = await _mediator.Send(new RegisterUserCommand(request.Model, request.Role));

            if (result.IsSuccess)
            {
                return new OkObjectResult("User registered successfully.");
            }

            return new BadRequestObjectResult("User registration failed.");
        }
    }
}
