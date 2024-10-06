using MediatR;
using ASP.NET_CORE_Project_1.Models;
using System.Security.Claims;

namespace ASP.NET_CORE_Project_1.Queries.Users
{
    public class GetCurrentUserQuery : IRequest<ApplicationUser>
    {
        public ClaimsPrincipal User { get; set; }

        public GetCurrentUserQuery(ClaimsPrincipal user)
        {
            User = user;
        }
    }
}
