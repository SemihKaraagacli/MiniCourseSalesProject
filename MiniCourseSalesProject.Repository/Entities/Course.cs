namespace MiniCourseSalesProject.Repository.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Foreign Key
        public Guid CategoryId { get; set; }

        // Kategori bilgisi
        public List<BasketItem> BasketItems { get; set; }
        public Category Category { get; set; }
    }
}
