﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("/api/users")]
        public async Task<IActionResult> Register([FromBody] SignUpModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _jwtTokenService.GenerateJwtTokenAsync(user);
                return CreatedAtAction(nameof(GetCurrentUser), new { id = user.Id }, new { Token = token });
            }

            return BadRequest(result.Errors.Select(e => e.Description).ToList());
        }

        [AllowAnonymous]
        [HttpPost("/api/auth/login")]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userName = User.Identity?.Name;
            return Ok(new { UserName = userName });
        }
    }
}
