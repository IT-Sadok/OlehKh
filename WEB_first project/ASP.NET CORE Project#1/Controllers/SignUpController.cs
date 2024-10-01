using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Services;
using Microsoft.AspNetCore.Authorization;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Constants;
using ASP.NET_CORE_Project_1.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Identity;


namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRegistrationService _registrationService;
        private readonly IMapper _mapper;
        private readonly IUserRegistrationService _userRegistrationService;

        public SignUpController(
            UserManager<ApplicationUser> userManager,
            IRegistrationService registrationService,
            IMapper mapper,
            IUserRegistrationService userRegistrationService)
        {
            _userManager = userManager;
            _registrationService = registrationService;
            _mapper = mapper;
            _userRegistrationService = userRegistrationService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] BaseSignUpModel model, [FromQuery] string role)
        {
            return await _userRegistrationService.RegisterUserAsync(model, role, User);
        }
    }
}
