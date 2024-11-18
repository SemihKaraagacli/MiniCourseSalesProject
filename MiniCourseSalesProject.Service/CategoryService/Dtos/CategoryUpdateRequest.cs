namespace MiniCourseSalesProject.Service.CategoryService.Dtos
{
    public record CategoryUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
