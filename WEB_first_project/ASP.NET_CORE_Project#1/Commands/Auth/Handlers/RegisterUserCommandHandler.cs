using MediatR;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Constants;
using ASP.NET_CORE_Project_1.Queries.Users;
using ASP.NET_CORE_Project_1.Commands.Auth;
using ASP.NET_CORE_Project_1.Commands.Users;
using ASP.NET_CORE_Project_1.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ASP.NET_CORE_Project_1.Commands.Auth.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegistrationResult>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public RegisterUserCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
        {
            _mediator = mediator;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RegistrationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new ValidateUserNameQuery(request.Model.UserName), cancellationToken);
            if (userExists)
            {
                return new RegistrationResult
                {
                    IsSuccess = false,
                    Errors = new List<string> { "UserName is already in use" }
                };
            }

            var createResult = await _mediator.Send(new CreateApplicationUserCommand(request.Model), cancellationToken);
            if (!createResult.Succeeded)
            {
                return new RegistrationResult
                {
                    IsSuccess = false,
                    Errors = createResult.Errors.Select(e => e.Description).ToList()
                };
            }

            var user = await _userManager.FindByNameAsync(request.Model.UserName);

            await _userManager.AddToRoleAsync(user, request.Role);

            var token = new JwtSecurityTokenHandler().WriteToken(await _jwtTokenService.GenerateJwtTokenAsync(user));

            //var token = await _jwtTokenService.GenerateJwtTokenAsync(user);

            return new RegistrationResult
            {
                IsSuccess = true,
                Token = token
            };
        }
    }
}
