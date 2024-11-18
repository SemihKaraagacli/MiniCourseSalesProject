using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Service.OrderService.Dtos
{
    public record OrderUpdateRequest
    {
        public Guid Id { get; set; }
        public List<Guid> CourseIds { get; set; }
    }
}
