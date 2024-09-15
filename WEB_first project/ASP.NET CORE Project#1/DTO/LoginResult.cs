using ASP.NET_CORE_Project_1.Models;

public class LoginResult
{
    public bool IsSuccess { get; set; }
    public ApplicationUser? User { get; set; }
    public string? Token { get; set; } // Додаємо поле для токена
    public IEnumerable<string>? Errors { get; set; }
}
