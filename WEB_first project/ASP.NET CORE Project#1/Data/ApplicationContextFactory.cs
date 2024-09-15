using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE_Project_1.Data
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            // Створюємо конфігурацію з appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Створюємо DbContextOptions для ApplicationContext
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var connectionString = configuration.GetConnectionString("Database");

            // Налаштовуємо підключення до PostgreSQL
            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
