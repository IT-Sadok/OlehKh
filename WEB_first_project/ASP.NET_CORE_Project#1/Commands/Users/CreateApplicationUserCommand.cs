using ASP.NET_CORE_Project_1.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Commands.Users
{
    public class CreateApplicationUserCommand : IRequest<IdentityResult>
    {
        public BaseSignUpModel Model { get; set; }

        public CreateApplicationUserCommand(BaseSignUpModel model)
        {
            Model = model;
        }
    }
}
