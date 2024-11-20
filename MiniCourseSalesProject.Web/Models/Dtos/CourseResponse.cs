namespace MiniCourseSalesProject.Web.Models.Dtos
{
    public record CourseResponse(Guid Id, string Name, string Description, decimal Price, string CategoryName, DateTime CreatedDate, DateTime UpdatedDate);
}
