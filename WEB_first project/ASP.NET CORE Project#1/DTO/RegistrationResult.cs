using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.DTO
{
    public class RegistrationResult
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
