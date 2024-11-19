namespace MiniCourseSalesProject.Service.CourseService.Dtos
{
    public record CourseCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
