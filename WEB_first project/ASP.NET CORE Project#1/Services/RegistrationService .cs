using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public RegistrationService(UserManager<ApplicationUser> userManager, ApplicationContext context, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RegistrationResult> RegisterUserAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var account = new Account
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                var token = await _jwtTokenService.GenerateJwtTokenAsync(user);

                return new RegistrationResult
                {
                    IsSuccess = true,
                    User = user,
                    Token = token
                };
            }

            return new RegistrationResult
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
    }


}
