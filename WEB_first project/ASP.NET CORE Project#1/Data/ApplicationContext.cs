using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Зв'язок між ApplicationUser і Order як Passenger
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.PassengerOrders)
                .WithOne(o => o.Passenger)
                .HasForeignKey(o => o.PassengerId)
                .OnDelete(DeleteBehavior.Restrict); // Забороняємо видалення пасажира, якщо є замовлення

            // Зв'язок між ApplicationUser і Order як Driver
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.DriverOrders)
                .WithOne(o => o.Driver)
                .HasForeignKey(o => o.DriverId)
                .OnDelete(DeleteBehavior.SetNull); // Якщо водія видалено, DriverId стане null
        }

    }
}
