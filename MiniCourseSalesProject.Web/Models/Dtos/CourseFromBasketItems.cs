namespace MiniCourseSalesProject.Web.Models.Dtos
{
    public record CourseFromBasketItems(Guid Id,Guid BasketId, string Name, string Description, decimal Price, string? CategoryName, DateTime CreatedDate, DateTime UpdatedDate);
}
