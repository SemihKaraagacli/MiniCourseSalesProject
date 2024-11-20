using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Service.PaymentService.Dtos
{
    public class PaymentUpdateRequest
    {
        public Guid Id { get; set; }
        public string PaymentStatus { get; set; }
    }
}
