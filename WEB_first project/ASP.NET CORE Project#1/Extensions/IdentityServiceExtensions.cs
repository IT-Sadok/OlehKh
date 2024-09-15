using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ASP.NET_CORE_Project_1.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Параметри паролю
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Налаштування блокування користувача
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Налаштування автентифікації
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            // Отримуємо налаштування JWT з конфігураційного файлу (appsettings.json)
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("Secret");

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentException("JWT Secret key is not configured.");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            // Налаштування JWT Bearer автентифікації
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Вимкнути HTTPS для тестових цілей, увімкніть у production
                options.SaveToken = true; // Зберігаємо токен
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Перевірка видавця токена
                    ValidateAudience = true, // Перевірка аудиторії токена
                    ValidateLifetime = true, // Перевірка терміну дії токена
                    ValidateIssuerSigningKey = true, // Перевірка підпису токена
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"), // Видавець
                    ValidAudience = jwtSettings.GetValue<string>("Audience"), // Аудиторія
                    IssuerSigningKey = new SymmetricSecurityKey(key) // Ключ підпису
                };
            });

            // Налаштування авторизації
            services.AddAuthorization(options =>
            {
                // Вимагаємо автентифікації для всіх запитів за замовчуванням
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .Build();
            });

            return services;
        }
    }
}
