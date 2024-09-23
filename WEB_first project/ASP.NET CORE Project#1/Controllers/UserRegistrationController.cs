using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Services;
using Microsoft.AspNetCore.Authorization;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Utils;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public UserRegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [AllowAnonymous]
        [HttpPost("passengers")]
        public async Task<IActionResult> RegisterPassenger([FromBody] PassengerSignUpModel model)
        {
            var result = await _registrationService.RegisterPassengerAsync(model);
            return RegistrationHelper.HandleRegistrationResult(result, this);
        }

        [AllowAnonymous]
        [HttpPost("drivers")]
        public async Task<IActionResult> RegisterDriver([FromBody] DriverSignUpModel model)
        {
            var result = await _registrationService.RegisterDriverAsync(model);
            return RegistrationHelper.HandleRegistrationResult(result, this);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admins")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminSignUpModel model)
        {
            var result = await _registrationService.RegisterAdminAsync(model);
            return RegistrationHelper.HandleRegistrationResult(result, this);
        }
    }
}
