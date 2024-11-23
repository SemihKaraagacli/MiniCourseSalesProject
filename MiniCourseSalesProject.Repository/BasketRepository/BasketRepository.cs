using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.BasketRepository
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;
        public BasketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Basket> GetBasketByUserAsync(Guid userId)
        {
            return await _context.Baskets.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Basket> GetBasketItemByUserAsync(Guid UserId)
        {
            return await _context.Baskets.Include(x => x.BasketItems).FirstOrDefaultAsync(x => x.UserId == UserId);
        }

        public async Task<Basket> GetBasketItemInCourseByUserAsync(Guid? UserId)
        {
            return await _context.Baskets.Include(x => x.BasketItems).ThenInclude(x => x.Course).FirstOrDefaultAsync(x => x.UserId == UserId);
        }
    }
}
