namespace MiniCourseSalesProject.Repository.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Amount { get; set; }

        // İlişkiler
        public Order Order { get; set; }
    }
}
