﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            if (Database.IsRelational())  // Перевірка, чи це реляційна база даних
            {
                Database.Migrate();  // Виконуємо міграції тільки для реляційної БД
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.PassengerOrders)
                .WithOne(o => o.Passenger)
                .HasForeignKey(o => o.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.DriverOrders)
                .WithOne(o => o.Driver)
                .HasForeignKey(o => o.DriverId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Account>()
                .HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
