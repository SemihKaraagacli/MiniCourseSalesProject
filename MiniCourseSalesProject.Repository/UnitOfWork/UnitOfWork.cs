
using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Repository.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<Basket> Attach(Basket basket)
        {
            context.Attach(basket);
            return basket;
        }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
