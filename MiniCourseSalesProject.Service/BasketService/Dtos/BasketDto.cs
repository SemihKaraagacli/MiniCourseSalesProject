using MiniCourseSalesProject.Service.CourseService.Dtos;

namespace MiniCourseSalesProject.Service.BasketService.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CourseDto> Courses { get; set; }
    }
}
