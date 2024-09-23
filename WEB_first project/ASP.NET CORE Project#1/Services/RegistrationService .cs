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


        public async Task<RegistrationResult> RegisterPassengerAsync(PassengerSignUpModel model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName не може бути порожнім" } };
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName вже використовується" } };
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Passenger");

                var account = new Account
                {
                    UserId = user.Id,
                    Role = "Passenger",
                    CreatedAt = DateTime.UtcNow,
                    Gender = model.Gender,
                    Age = model.Age
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

        public async Task<RegistrationResult> RegisterDriverAsync(DriverSignUpModel model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName не може бути порожнім" } };
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName вже використовується" } };
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Driver");

                var account = new Account
                {
                    UserId = user.Id,
                    Role = "Driver",
                    DrivingExperienceYears = model.Experience,
                    CreatedAt = DateTime.UtcNow,
                    Gender = model.Gender,
                    Age = model.Age,
                    CarModel = model.CarModel
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

        public async Task<RegistrationResult> RegisterAdminAsync(AdminSignUpModel model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName не може бути порожнім" } };
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName вже використовується" } };
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");

                var account = new Account
                {
                    UserId = user.Id,
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    Gender = model.Gender,
                    Age = model.Age
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
