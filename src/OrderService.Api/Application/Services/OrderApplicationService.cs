using MassTransit;
using OrderService.Api.Application.DTOs;
using OrderService.Api.Application.Interfaces;
using OrderService.Api.Domain.Repositories;
using OrderService.Shared.Domain.Entities;
using OrderService.Shared.Events;

namespace OrderService.Api.Application.Services
{
    public class OrderApplicationService : IOrderApplicationService
    {
        private readonly IOrderRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderApplicationService> _logger;

        public OrderApplicationService(IOrderRepository repository, IPublishEndpoint publishEndpoint, ILogger<OrderApplicationService> logger)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<List<OrderResponse>> GetAllAsync()
        {
            var orders = await _repository.GetAllAsync();
            return orders.Select(order => new OrderResponse
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Description = order.Description,
                Value = order.Value,
                Status = order.Status.ToString()
            }).ToList();
        }

        public async Task<OrderResponse> CreateAsync(string customerName, string description, decimal value)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = customerName,
                Description = description,
                Value = value,
                Status = Shared.Enums.OrderStatus.Pending
            };

            await _repository.AddAsync(order);

            var response = new OrderResponse
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Description = order.Description,
                Value = order.Value,
                Status = order.Status.ToString(),
            };

            _ = Task.Run(async () =>
            {
                try
                {
                    await _publishEndpoint.Publish(new OrderCreatedEvent
                    {
                        OrderId = order.Id
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Erro ao publicar o evento do pedido {order.Id}");
                }
            });

            return response;
        }

        public async Task<OrderResponse?> GetByIdAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                return null;

            var response = new OrderResponse
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Description = order.Description,
                Value = order.Value,
                Status = order.Status.ToString()
            };

            return response;
        }
    }
}
