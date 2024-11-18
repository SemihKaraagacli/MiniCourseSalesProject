namespace MiniCourseSalesProject.Service.CourseService.Dtos
{
    public record CourseUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}
