using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1
{
    public interface IAccountService
    {
        Task<bool> CreateAccountAsync(RegisterModel model);
    }
}
