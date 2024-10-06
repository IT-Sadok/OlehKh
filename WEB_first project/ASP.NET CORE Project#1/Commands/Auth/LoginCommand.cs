using MediatR;
using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Commands.Auth
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }

}
