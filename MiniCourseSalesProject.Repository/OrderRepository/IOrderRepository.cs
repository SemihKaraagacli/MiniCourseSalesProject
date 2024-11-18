using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.OrderRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // Siparişe özgü ek işlemler
        Task<List<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}
