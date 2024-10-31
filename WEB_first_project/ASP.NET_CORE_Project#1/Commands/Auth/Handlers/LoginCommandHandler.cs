using MediatR;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ASP.NET_CORE_Project_1.Commands.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user != null)
                {

                    var token = new JwtSecurityTokenHandler().WriteToken(await _jwtTokenService.GenerateJwtTokenAsync(user));

                    //var token = await _jwtTokenService.GenerateJwtTokenAsync(user);

                    return new LoginResult
                    {
                        IsSuccess = true,
                        User = user,
                        Token = token
                    };
                }
            }

            return new LoginResult
            {
                IsSuccess = false,
                Errors = new[] { "Invalid login attempt." }
            };
        }
    }
}
