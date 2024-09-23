using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginUserAsync(SignInModel model);
    }

}
