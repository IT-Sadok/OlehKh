using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await _accountService.CreateAccountAsync(model);

            if (result)
            {
                return Ok("Account created successfully");
            }

            return BadRequest("Error creating account");
        }
    }
}
