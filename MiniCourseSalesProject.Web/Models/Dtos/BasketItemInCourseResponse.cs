namespace MiniCourseSalesProject.Web.Models.Dtos
{
    public record BasketItemInCourseResponse(Guid BasketId, Guid OrderId, Guid BasketItemId, Guid Id, string Name, string Description, decimal Price, DateTime CreatedDate, DateTime UpdatedDate);
}
