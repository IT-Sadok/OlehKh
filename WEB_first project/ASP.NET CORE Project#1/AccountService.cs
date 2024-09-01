using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _context;

        public AccountService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAccountAsync()
        {
            await _context.Accounts.AddAsync(entity: new Account
            {
                Username = "o.kharchenko",
                Password = "12345",
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
    }

     
}
