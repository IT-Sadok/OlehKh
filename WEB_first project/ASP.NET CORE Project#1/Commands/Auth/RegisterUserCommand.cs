using MediatR;
using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Commands.Auth
{
    public class RegisterUserCommand : IRequest<RegistrationResult>
    {
        public BaseSignUpModel Model { get; set; }
        public string Role { get; set; }

        public RegisterUserCommand(BaseSignUpModel model, string role)
        {
            Model = model;
            Role = role;
        }
    }
}
