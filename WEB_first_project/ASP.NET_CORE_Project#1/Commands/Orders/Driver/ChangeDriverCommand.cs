using MediatR;

namespace ASP.NET_CORE_Project_1.Commands.Orders.Driver
{
    public class ChangeDriverCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public Guid NewDriverId { get; set; }

        public ChangeDriverCommand(int orderId, Guid newDriverId)
        {
            OrderId = orderId;
            NewDriverId = newDriverId;
        }
    }
}
