using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Services;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILoginService _loginService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        public AccountController(
            IRegistrationService registrationService,
            ILoginService loginService,
            IJwtTokenService jwtTokenService,
            IConfiguration configuration) // Додаємо IConfiguration в конструктор
        {
            _registrationService = registrationService;
            _loginService = loginService;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration; // Ініціалізуємо _configuration
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignUpModel model)
        {
            var result = await _registrationService.RegisterUserAsync(model);

            if (result.IsSuccess)
            {
                var token = await _jwtTokenService.GenerateJwtTokenAsync(result.User);
                return Ok(new { Token = token });
            }

            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            var result = await _loginService.LoginUserAsync(model);

            if (result.IsSuccess)
            {
                var token = await _jwtTokenService.GenerateJwtTokenAsync(result.User);
                return Ok(new { Token = token });
            }

            return BadRequest(result.Errors);
        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Secret"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = jwtSettings.GetValue<string>("Issuer"),
                Audience = jwtSettings.GetValue<string>("Audience"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
