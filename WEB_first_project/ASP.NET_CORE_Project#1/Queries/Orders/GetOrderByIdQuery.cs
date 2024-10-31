using MediatR;
using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Queries.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderDTO?>
    {
        public int Id { get; set; }

        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }
    }
}
