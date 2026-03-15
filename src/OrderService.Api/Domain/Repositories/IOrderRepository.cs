
using OrderService.Shared.Domain.Entities;

namespace OrderService.Api.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task UpdateAsync(Order order);
    }
}
