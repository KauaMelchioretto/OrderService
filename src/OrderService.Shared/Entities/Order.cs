using OrderService.Shared.Enums;

namespace OrderService.Shared.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public OrderStatus Status { get; set; }
    }
}
