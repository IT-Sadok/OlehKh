using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE_Project_1.DTO
{
    public class SignUpModel
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
