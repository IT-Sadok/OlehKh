using MediatR;

namespace ASP.NET_CORE_Project_1.Commands.Orders
{
    public class AssignDriverCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public Guid DriverId { get; set; }

        public AssignDriverCommand(int orderId, Guid driverId)
        {
            OrderId = orderId;
            DriverId = driverId;
        }
    }
}
