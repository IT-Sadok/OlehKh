using MediatR;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Queries.Users.Handlers
{
    public class ValidateUserNameQueryHandler : IRequestHandler<ValidateUserNameQuery, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ValidateUserNameQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ValidateUserNameQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByNameAsync(request.UserName) != null;
        }
    }
}
