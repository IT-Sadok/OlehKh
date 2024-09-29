namespace ASP.NET_CORE_Project_1.Services
{
    public interface IChangeDriverService
    {
        Task<bool> ChangeDriverAsync(int orderId, Guid newDriverId);
    }
}

