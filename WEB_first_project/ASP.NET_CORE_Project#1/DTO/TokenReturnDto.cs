namespace ASP.NET_CORE_Project_1.DTO
{
    public class TokenReturnDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
