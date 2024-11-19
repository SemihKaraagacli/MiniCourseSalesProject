using MiniCourseSalesProject.Service.PaymentService.Dtos;

namespace MiniCourseSalesProject.Service.PaymentService
{
    public interface IPaymentService
    {
        Task<ServiceResult<PaymentDto>> ProcessPaymentAsync(PaymentCreateRequest request);
    }
}