using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;

public interface IRegistrationService
{
    Task<RegistrationResult> RegisterPassengerAsync(PassengerSignUpModel model);

    Task<RegistrationResult> RegisterDriverAsync(DriverSignUpModel model);

    Task<RegistrationResult> RegisterAdminAsync(AdminSignUpModel model);
}
