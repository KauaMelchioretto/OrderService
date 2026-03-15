using Microsoft.EntityFrameworkCore;
using OrderService.Shared.Domain.Entities;

namespace OrderService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<Order> Orders => Set<Order>();
    }
}
