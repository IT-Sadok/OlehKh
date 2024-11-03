using MediatR;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Commands.Users.Handlers
{
    public class CreateApplicationUserCommandHandler : IRequestHandler<CreateApplicationUserCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateApplicationUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Model.UserName,
                Name = request.Model.Name,
                Email = request.Model.Email,
                PhoneNumber = request.Model.PhoneNumber
            };

            return await _userManager.CreateAsync(user, request.Model.Password);
        }
    }
}
