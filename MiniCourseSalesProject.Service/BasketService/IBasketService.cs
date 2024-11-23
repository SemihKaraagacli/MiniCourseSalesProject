
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Service.BasketService.Dtos;
using MiniCourseSalesProject.Service.CourseService.Dtos;

namespace MiniCourseSalesProject.Service.BasketService
{
    public interface IBasketService
    {
        Task<ServiceResult<Guid>> CreateBasketAsync(BasketCreateRequest request);
        Task<ServiceResult> DeleteBasketAsync(Guid BasketId);
        Task<ServiceResult> DeleteCourseFromBasketAsync(Guid UserId, Guid CourseId);
        Task<ServiceResult<List<BasketItemInCourseResponse>>> GetBasketItemInCourseAsync(Guid UserId);
    }
}
