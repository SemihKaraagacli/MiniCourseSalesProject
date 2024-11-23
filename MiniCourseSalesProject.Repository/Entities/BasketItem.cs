namespace MiniCourseSalesProject.Repository.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Guid? BasketId { get; set; }
        public Guid CourseId { get; set; }
        public int Quantity { get; set; }
        public Basket? Basket { get; set; }
        public Course Course { get; set; }
    }
}
