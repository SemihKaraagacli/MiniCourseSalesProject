using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.BasketRepository
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<Basket> GetBasketByUserAsync(Guid userId);
        Task<Basket> GetBasketItemByUserAsync(Guid UserId);
        Task<Basket> GetBasketItemInCourseByUserAsync(Guid? UserId);
    }
}
