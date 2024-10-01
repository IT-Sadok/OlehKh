using System.Threading.Tasks;

namespace ASP.NET_CORE_Project_1.Services
{
    public interface IRemoveDriverFromOrderService
    {
        Task<(bool IsSuccess, string ErrorMessage)> RemoveDriverAsync(int orderId, string userId, string role);
    }
}
