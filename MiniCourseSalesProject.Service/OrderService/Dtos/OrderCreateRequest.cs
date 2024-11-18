namespace MiniCourseSalesProject.Service.OrderService.Dtos
{
    public record OrderCreateRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> CourseId { get; set; }
    }
}
