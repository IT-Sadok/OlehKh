using MediatR;
using ASP.NET_CORE_Project_1.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE_Project_1.Commands.Auth
{
    public class RegisterUserWithRoleCommand : IRequest<IActionResult>
    {
        public BaseSignUpModel Model { get; set; }
        public string Role { get; set; }
        public ClaimsPrincipal CurrentUser { get; set; }

        public RegisterUserWithRoleCommand(BaseSignUpModel model, string role, ClaimsPrincipal currentUser)
        {
            Model = model;
            Role = role;
            CurrentUser = currentUser;
        }
    }
}
