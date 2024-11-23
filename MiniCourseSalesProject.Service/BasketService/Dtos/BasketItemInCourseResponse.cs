namespace MiniCourseSalesProject.Service.BasketService.Dtos
{
    public class BasketItemInCourseResponse
    {
        public Guid Id { get; set; }
        public Guid? BasketId { get; set; }
        public Guid BasketItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
