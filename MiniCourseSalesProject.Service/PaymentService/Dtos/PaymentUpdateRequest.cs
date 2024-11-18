using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Service.PaymentService.Dtos
{
    public class PaymentUpdateRequest
    {
        public Guid Id { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
