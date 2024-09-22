using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE_Project_1.DTO
{
    public class DriverSignUpModel: BaseSignUpModel
    {
        [Required]
        public int Experience { get; set; }

        public string? CarModel { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
    }
}
