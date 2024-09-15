using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;

public interface IRegistrationService
{
    Task<RegistrationResult> RegisterUserAsync(SignUpModel model);
}
