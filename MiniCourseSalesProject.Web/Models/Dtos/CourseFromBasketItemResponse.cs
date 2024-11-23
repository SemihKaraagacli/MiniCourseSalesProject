namespace MiniCourseSalesProject.Web.Models.Dtos
{
    public record CourseFromBasketItemResponse(Guid BasketId, Guid BasketItemId, Guid Id, string Name, string Description, decimal Price, DateTime CreatedDate, DateTime UpdatedDate);
}
