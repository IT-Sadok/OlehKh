using MediatR;

namespace ASP.NET_CORE_Project_1.Commands.Orders
{
    public class CompleteOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }

        public CompleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
