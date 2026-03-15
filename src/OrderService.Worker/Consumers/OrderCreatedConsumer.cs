using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Data;
using OrderService.Shared.Events;

namespace OrderService.Worker.Consumers
{
    internal class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly AppDbContext _context;

        public OrderCreatedConsumer(AppDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            try
            {
                var orderId = context.Message.OrderId;

                var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

                if (order == null)
                    return;

                Console.WriteLine($"Processando pedido {order.Id}");

                await Task.Delay(5000);

                order.Status = Shared.Enums.OrderStatus.Processed;

                await _context.SaveChangesAsync();

                Console.WriteLine($"Pedido {orderId} processado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar pedido: {ex.Message}");
                throw;
            }
        }
    }
}
