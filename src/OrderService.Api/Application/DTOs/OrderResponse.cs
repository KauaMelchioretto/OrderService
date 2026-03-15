namespace OrderService.Api.Application.DTOs
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Status { get; set; }
    }
}
