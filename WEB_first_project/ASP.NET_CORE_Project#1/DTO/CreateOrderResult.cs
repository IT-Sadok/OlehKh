namespace ASP.NET_CORE_Project_1.DTO
{
    public class CreateOrderResult
    {
        public bool IsSuccess { get; set; }
        public OrderDTO? Order { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
