namespace MiniCourseSalesProject.Service.PaymentService.Dtos
{
    public class PaymentCreateRequest
    {
        public Guid userId { get; set; }
        public Guid OrderId { get; set; }
    }
}
