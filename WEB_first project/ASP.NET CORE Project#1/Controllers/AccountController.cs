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

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            ILoginService loginService,
            IJwtTokenService jwtTokenService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _loginService = loginService;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
            _userManager = userManager;
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
        public async Task<IActionResult> GetCurrentUser()
        {
            // Отримуємо поточного користувача за допомогою UserManager
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); // Якщо користувача не знайдено
            }

            // Отримуємо ролі користувача напряму з бази даних
            var roles = await _userManager.GetRolesAsync(user);

            // Повертаємо дані користувача разом з Name, UserName і ролями
            return Ok(new
            {
                Name = user.Name, // Використовуємо поле Name
                UserName = user.UserName, // UserName також додаємо
                Roles = roles // Список ролей
            });
        }
    }
}










//public IActionResult GetCurrentUser()
//{
//    var userName = User.Identity?.Name;
//    var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
//    return Ok(new { UserName = userName, Roles = roles });
//}

