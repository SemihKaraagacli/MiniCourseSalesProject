namespace MiniCourseSalesProject.Web.Models.Dtos
{
    public record PaymentResponse(Guid OrderId, DateTime PaymentDate, string PaymentStatus, decimal Amount);
}