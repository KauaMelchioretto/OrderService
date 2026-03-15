using OrderService.Api.Application.DTOs;

namespace OrderService.Api.Application.Interfaces
{
    public interface IOrderApplicationService
    {
        Task<List<OrderResponse>> GetAllAsync();
        Task<OrderResponse> CreateAsync(string customerName, string description, decimal value);
        Task<OrderResponse?> GetByIdAsync(Guid id);
    }
}
