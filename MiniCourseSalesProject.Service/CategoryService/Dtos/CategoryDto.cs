namespace MiniCourseSalesProject.Service.CategoryService.Dtos
{
    public record CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
