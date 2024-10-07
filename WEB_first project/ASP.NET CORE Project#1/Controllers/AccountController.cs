using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MediatR;
using System.Security.Claims;
using ASP.NET_CORE_Project_1.Commands.Auth;
using ASP.NET_CORE_Project_1.Queries.Users;
using System.IdentityModel.Tokens.Jwt;
using ASP.NET_CORE_Project_1.Services;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountController(IMediator mediator, IJwtTokenService jwtTokenService)
        {
            _mediator = mediator;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            var result = await _mediator.Send(new LoginCommand(model.UserName, model.Password));

            if (result.IsSuccess && result.User != null)
            {
                var token = await _jwtTokenService.GenerateJwtTokenAsync(result.User);

                return Ok(new TokenReturnDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                });
            }

            return BadRequest(result.Errors);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _mediator.Send(new GetCurrentUserQuery(User));

            if (user == null)
            {
                return Unauthorized();
            }

            var roles = await _mediator.Send(new GetUserRolesQuery(user));

            return Ok(new
            {
                Name = user.Name,
                UserName = user.UserName,
                Roles = roles
            });
        }
    }
}