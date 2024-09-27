using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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


        public async Task<RegistrationResult> RegisterUserAsync(BaseSignUpModel model, string role)
        {
            // Валідація
            if (string.IsNullOrEmpty(model.UserName))
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName не може бути порожнім" } };
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName вже використовується" } };
            }

            // Створюємо нового користувача
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Додаємо користувача до потрібної ролі
                await _userManager.AddToRoleAsync(user, role);

                // Створюємо акаунт в залежності від ролі
                var account = new Account
                {
                    UserId = user.Id,
                    Role = role,
                    CreatedAt = DateTime.UtcNow,
                    Gender = model.Gender,
                    Age = model.Age
                };

                // Якщо це водій, додаємо додаткові поля
                if (role == "Driver" && model is DriverSignUpModel driverModel)
                {
                    account.DrivingExperienceYears = driverModel.Experience;
                    account.CarModel = driverModel.CarModel;
                }

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                // Генерація JWT-токену
                var token = await _jwtTokenService.GenerateJwtTokenAsync(user);

                return new RegistrationResult
                {
                    IsSuccess = true,
                    User = user,
                    Token = token
                };
            }

            // Якщо реєстрація не вдалася, повертаємо помилки
            return new RegistrationResult
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
    }
}
