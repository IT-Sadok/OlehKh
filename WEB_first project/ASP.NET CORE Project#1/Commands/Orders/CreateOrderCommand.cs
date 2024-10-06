using ASP.NET_CORE_Project_1.DTO;
using MediatR;

namespace ASP.NET_CORE_Project_1.Commands.Orders
{
    public class CreateOrderCommand : IRequest<OrderDTO>
    {
        public Guid PassengerId { get; set; }
        public string PickupLocation { get; set; }
        public string Destination { get; set; }

        public CreateOrderCommand(Guid passengerId, string pickupLocation, string destination)
        {
            PassengerId = passengerId;
            PickupLocation = pickupLocation;
            Destination = destination;
        }
    }
}
