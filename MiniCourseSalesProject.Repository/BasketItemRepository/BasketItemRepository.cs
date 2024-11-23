using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.BasketItemRepository
{
    public class BasketItemRepository : GenericRepository<BasketItem>, IBasketItemRepository
    {
        private readonly AppDbContext _context;
        public BasketItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BasketItem> GetItemsByBasketIdAsync(Guid? basketId)
        {
            return await _context.BasketItems.Include(x => x.Course).FirstOrDefaultAsync(x => x.BasketId == basketId);
        }
    }
}
