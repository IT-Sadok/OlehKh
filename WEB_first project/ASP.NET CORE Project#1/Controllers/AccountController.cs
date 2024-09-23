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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Utils;

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
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            IRegistrationService registrationService,
            ILoginService loginService,
            IJwtTokenService jwtTokenService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _registrationService = registrationService;
            _loginService = loginService;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("/api/passengers")]
        public async Task<IActionResult> RegisterPassenger([FromBody] PassengerSignUpModel model)
        {
            var result = await _registrationService.RegisterPassengerAsync(model);
            return RegistrationHelper.HandleRegistrationResult(result, this);
        }

        [AllowAnonymous]
        [HttpPost("/api/drivers")]
        public async Task<IActionResult> RegisterDriver([FromBody] DriverSignUpModel model)
        {
            var result = await _registrationService.RegisterDriverAsync(model);
            return RegistrationHelper.HandleRegistrationResult(result, this);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/admins")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminSignUpModel model)
        {
            var result = await _registrationService.RegisterAdminAsync(model);
            return RegistrationHelper.HandleRegistrationResult(result, this);
        }

        [AllowAnonymous]
        [HttpPost("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            var result = await _loginService.LoginUserAsync(model);

            if (result.IsSuccess && result.User != null)
            {
                var token = await _jwtTokenService.GenerateJwtTokenAsync(result.User);
                return Ok(new { Token = token });
            }

            return BadRequest(result.Errors);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userName = User.Identity?.Name;
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            return Ok(new { UserName = userName, Roles = roles });
        }
    }
}
