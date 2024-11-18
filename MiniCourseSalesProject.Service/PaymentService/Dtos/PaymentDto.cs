using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Service.PaymentService.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal Amount { get; set; }
    }
}