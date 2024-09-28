using ASP.NET_CORE_Project_1.Constants;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        private async Task<IdentityResult> CreateApplicationUser(BaseSignUpModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<RegistrationResult> RegisterUserAsync(BaseSignUpModel model, string role)
        {
            var validRoles = new List<string> { UserRoles.Admin, UserRoles.Driver, UserRoles.Passenger };
            if (!validRoles.Contains(role))
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "Invalid role" } };
            }

            if (!ValidateModel(model, out var validationErrors))
            {
                return new RegistrationResult { IsSuccess = false, Errors = validationErrors };
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "UserName is already in use" } };
            }

            var result = await CreateApplicationUser(model);
            if (!result.Succeeded)
            {
                return new RegistrationResult { IsSuccess = false, Errors = result.Errors.Select(e => e.Description).ToList() };
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            await _userManager.AddToRoleAsync(user, role);

            var account = new Account
            {
                UserId = user.Id,
                Role = role,
                CreatedAt = DateTime.UtcNow,
                Gender = model.Gender,
                Age = model.Age
            };

            if (role == UserRoles.Driver && model is DriverSignUpModel driverModel)
            {
                account.DrivingExperienceYears = driverModel.Experience;
                account.CarModel = driverModel.CarModel;
            }

            try
            {
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new RegistrationResult { IsSuccess = false, Errors = new List<string> { "Error saving data to the database", ex.Message } };
            }

            var token = await _jwtTokenService.GenerateJwtTokenAsync(user);

            return new RegistrationResult { IsSuccess = true, User = user, Token = token };
        }

        private bool ValidateModel(object model, out List<string> errors)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model);                                             // inner method checking if all limits are met
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
            return isValid;
        }

    }
}
