namespace MiniCourseSalesProject.Web.Models.ViewModels
{
    public class BasketCreateViewModel
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
