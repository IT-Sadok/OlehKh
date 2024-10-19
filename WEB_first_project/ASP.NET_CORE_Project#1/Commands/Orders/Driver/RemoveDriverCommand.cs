using MediatR;

namespace ASP.NET_CORE_Project_1.Commands.Orders.Driver
{
    public class RemoveDriverCommand : IRequest<(bool IsSuccess, string ErrorMessage)>
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }

        public RemoveDriverCommand(int orderId, string userId, string role)
        {
            OrderId = orderId;
            UserId = userId;
            Role = role;
        }
    }
}
