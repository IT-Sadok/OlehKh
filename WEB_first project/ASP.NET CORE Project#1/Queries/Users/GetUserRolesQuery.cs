using MediatR;
using System.Collections.Generic;
using ASP.NET_CORE_Project_1.Models;
using global::ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Queries.Users
{
    public class GetUserRolesQuery : IRequest<IList<string>>
    {
        public ApplicationUser User { get; set; }

        public GetUserRolesQuery(ApplicationUser user)
        {
            User = user;
        }
    }
}