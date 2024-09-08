using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _context;

        public AccountService(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateAccountAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Додати запис в таблицю Account
                var account = new Account
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}