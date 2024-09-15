using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_CORE_Project_1.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService; // Додаємо залежність для JWT токена

        // Конструктор для інжекції залежностей
        public LoginService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService; // Ініціалізуємо jwtTokenService
        }

        // Реалізація методу LoginUserAsync
        public async Task<LoginResult> LoginUserAsync(SignInModel model)
        {
            // Перевірка логіна і пароля
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    // Генерація JWT токена після успішного входу
                    var token = await _jwtTokenService.GenerateJwtTokenAsync(user);

                    return new LoginResult
                    {
                        IsSuccess = true,
                        User = user,
                        Token = token // Додаємо токен у результат
                    };
                }
            }

            // Повертаємо помилку в разі неуспішної авторизації
            return new LoginResult
            {
                IsSuccess = false,
                Errors = new[] { "Invalid login attempt." }
            };
        }
    }
}
