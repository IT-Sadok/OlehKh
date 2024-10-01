using ASP.NET_CORE_Project_1.DTO;
using System;
using System.Threading.Tasks;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface IUpdateUserService
    {
        Task<(bool IsSuccess, string ErrorMessage)> UpdateUserAsync(Guid userId, UpdateUserModel model, string currentUserId, bool isAdmin);
    }
}
