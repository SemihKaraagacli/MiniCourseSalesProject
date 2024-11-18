namespace MiniCourseSalesProject.Repository.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // İlişkiler
        public List<Course> Courses { get; set; }
    }
}
