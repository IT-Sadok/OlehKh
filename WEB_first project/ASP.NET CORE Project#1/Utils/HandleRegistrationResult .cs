using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Utils
{
    public static class RegistrationHelper
    {
        public static IActionResult HandleRegistrationResult(RegistrationResult result, ControllerBase controller)
        {
            if (result.IsSuccess && result.User != null)
            {
                return controller.CreatedAtAction("GetCurrentUser", "Account", new { }, new { Token = result.Token });

            }
            else
            {
                var errorMessages = result.Errors != null ? string.Join(", ", result.Errors) : "User information is missing.";
                return controller.BadRequest(errorMessages);
            }

        }
    }
}
