using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE_Project_1.DTO
{
    public class PassengerSignUpModel: BaseSignUpModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
    }
}
