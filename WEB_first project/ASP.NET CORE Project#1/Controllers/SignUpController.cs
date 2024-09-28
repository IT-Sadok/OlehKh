using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Services;
using Microsoft.AspNetCore.Authorization;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Constants;
using ASP.NET_CORE_Project_1.Mappings;
using AutoMapper;


namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : BaseController
    {
        private readonly IRegistrationService _registrationService;
        private readonly IMapper _mapper;

        public SignUpController(IRegistrationService registrationService, IMapper mapper)
        {
            _registrationService = registrationService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] BaseSignUpModel model, [FromQuery] string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return BadRequest("Role is required");
            }

            switch (role.ToLower())
            {
                case var r when r == UserRoles.Passenger.ToLower():
                    return await RegisterWithRoleAsync(model, UserRoles.Passenger);
                case var r when r == UserRoles.Driver.ToLower():
                    return await RegisterWithRoleAsync(model, UserRoles.Driver);
                case var r when r == UserRoles.Admin.ToLower():
                    return await RegisterWithRoleAsync(model, UserRoles.Admin);
                default:
                    return BadRequest("Invalid role provided");
            }
        }

        private async Task<IActionResult> RegisterWithRoleAsync(BaseSignUpModel model, string role)
        {
            var result = await _registrationService.RegisterUserAsync(model, role);
            return HandleRegistrationResult(result);
        }
    }
}
