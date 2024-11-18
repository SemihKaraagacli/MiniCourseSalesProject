namespace MiniCourseSalesProject.Repository.Entities
{
    public enum OrderStatus
    {
        Pending,    // Sipariş bekliyor
        Completed,  // Sipariş tamamlandı
        Failed,     // Sipariş başarısız
        Canceled    // Sipariş iptal edildi
    }
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> CourseId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }

        // İlişkiler
        public AppUser User { get; set; }
        public List<Course> Courses { get; set; }
        public Payment Payment { get; set; }
    }
}
