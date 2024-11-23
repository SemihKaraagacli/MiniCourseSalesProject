using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.OrderRepository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Kullanıcıya ait siparişleri getir
        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).Include(x => x.User).ToListAsync();
        }
    }
}