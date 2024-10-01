using ASP.NET_CORE_Project_1.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

public interface IUserRegistrationService
{
    Task<IActionResult> RegisterUserAsync(BaseSignUpModel model, string role, ClaimsPrincipal user);
}
