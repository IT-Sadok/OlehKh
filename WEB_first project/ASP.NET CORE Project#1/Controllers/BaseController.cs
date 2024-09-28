using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE_Project_1.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleRegistrationResult(RegistrationResult result)
        {
            if (result.IsSuccess && result.User != null)
            {
                return CreatedAtAction("GetCurrentUser", "Account", new { }, new { Token = result.Token });
            }
            else
            {
                var errorMessages = result.Errors != null ? string.Join(", ", result.Errors) : "User information is missing.";
                return BadRequest(errorMessages);
            }
        }
    }
}
