using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE_Project_1.DTO
{
    public class SignInModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
