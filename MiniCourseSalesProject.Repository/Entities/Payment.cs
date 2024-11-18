namespace MiniCourseSalesProject.Repository.Entities
{
    public enum PaymentStatus
    {
        Pending,    // Ödeme bekliyor
        Completed,  // Ödeme tamamlandı
        Failed,     // Ödeme başarısız
        Canceled    // Ödeme iptal edildi
    }

    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal Amount { get; set; }

        // İlişkiler
        public virtual Order Order { get; set; }
    }
}
