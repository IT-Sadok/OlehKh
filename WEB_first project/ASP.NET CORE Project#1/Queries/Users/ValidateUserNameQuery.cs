using MediatR;

namespace ASP.NET_CORE_Project_1.Queries.Users
{
    public class ValidateUserNameQuery : IRequest<bool>
    {
        public string UserName { get; set; }

        public ValidateUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
