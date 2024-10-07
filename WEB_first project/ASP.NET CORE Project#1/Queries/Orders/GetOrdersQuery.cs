using ASP.NET_CORE_Project_1.DTO;
using MediatR;

namespace ASP.NET_CORE_Project_1.Queries.Orders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDTO>>
    {
    }
}
