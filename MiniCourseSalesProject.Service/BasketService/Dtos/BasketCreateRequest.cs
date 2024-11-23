using MiniCourseSalesProject.Service.CourseService.Dtos;

namespace MiniCourseSalesProject.Service.BasketService.Dtos
{
    public record BasketCreateRequest(Guid UserId, Guid CourseId, int Quantity);
}
