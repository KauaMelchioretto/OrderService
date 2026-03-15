namespace OrderService.Api.Application.DTOs
{
    public class CreateOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}
