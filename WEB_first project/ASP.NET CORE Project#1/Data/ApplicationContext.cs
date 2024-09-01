using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts => Set<Account>();
    }
}
