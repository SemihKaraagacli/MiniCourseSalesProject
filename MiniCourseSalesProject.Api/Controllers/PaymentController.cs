using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service.PaymentService.Dtos;
using MiniCourseSalesProject.Service.PaymentService;

namespace MiniCourseSalesProject.Api.Controllers
{
    public class PaymentController(IPaymentService paymentService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ProcessPaymentAsync(PaymentCreateRequest request)
        {
            var result = await paymentService.ProcessPaymentAsync(request);
            return CreateObjectResult(result);

        }
    }
}
