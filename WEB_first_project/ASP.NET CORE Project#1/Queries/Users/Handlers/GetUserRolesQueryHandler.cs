using MediatR;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ASP.NET_CORE_Project_1.Services;

namespace ASP.NET_CORE_Project_1.Queries.Users.Handlers
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<string>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserRolesQueryHandler(ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<List<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.GetCurrentUser();
            if (user == null)
            {
                return new List<string>();
            }

            return (await _userManager.GetRolesAsync(user)).ToList();
        }
    }
}
