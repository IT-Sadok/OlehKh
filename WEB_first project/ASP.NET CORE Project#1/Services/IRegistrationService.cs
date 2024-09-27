using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;
using System.Threading.Tasks;

public interface IRegistrationService
{
    // Єдиний метод реєстрації для всіх ролей
    Task<RegistrationResult> RegisterUserAsync(BaseSignUpModel model, string role);
}
