using MiniCourseSalesProject.Service.CourseService.Dtos;

namespace MiniCourseSalesProject.Service.BasketService.Dtos
{
    public record BasketUpdateRequest(Guid Id, List<Guid> CourseIds);
}
