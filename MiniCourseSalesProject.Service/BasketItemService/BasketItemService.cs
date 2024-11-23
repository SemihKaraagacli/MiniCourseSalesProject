using MiniCourseSalesProject.Repository.BasketItemRepository;
using MiniCourseSalesProject.Repository.BasketRepository;
using MiniCourseSalesProject.Repository.CourseRepository;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.BasketItemService.Dtos;

namespace MiniCourseSalesProject.Service.BasketItemService
{
    public class BasketItemService(IBasketItemRepository basketItemRepository, IBasketRepository basketRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        //public async Task<ServiceResult<Guid>> AddToBasketItem(BasketItemCreateRequest request)
        //{

        //}
    }
}
