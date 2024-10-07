using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Authorization;
using ASP.NET_CORE_Project_1.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MediatR;
using ASP.NET_CORE_Project_1.Commands.Users;
using ASP.NET_CORE_Project_1.Commands.Auth;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SignUpController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IMediator mediator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] BaseSignUpModel model, [FromQuery] string role)
        {
            var result = await _mediator.Send(new RegisterUserCommand(model, role));

            if (result.IsSuccess)
            {
                return CreatedAtAction("GetCurrentUser", "Account", new { }, new { Token = result.Token });
            }

            return BadRequest(result.Errors);
        }
    }
}
