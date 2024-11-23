using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.BasketItemRepository
{
    public interface IBasketItemRepository : IGenericRepository<BasketItem>
    {
        Task<BasketItem> GetItemsByBasketIdAsync(Guid? basketId);
    }
}
