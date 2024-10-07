using MediatR;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.NET_CORE_Project_1.Queries.Users.Handlers
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IList<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserRolesQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.GetRolesAsync(request.User);
        }
    }
}
