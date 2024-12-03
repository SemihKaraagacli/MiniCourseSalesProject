using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service.PaymentService.Dtos;
using MiniCourseSalesProject.Service.PaymentService;
using Microsoft.AspNetCore.Authorization;

namespace MiniCourseSalesProject.Api.Controllers
{
    [Authorize]
    public class PaymentController(IPaymentService paymentService, ILogger<CustomControllerBase> logger, ILoggerFactory loggerFactory) : CustomControllerBase(logger, loggerFactory)
    {
        [HttpPost]
        public async Task<IActionResult> ProcessPaymentAsync(PaymentCreateRequest request)
        {
            var result = await paymentService.ProcessPaymentAsync(request);
            return CreateObjectResult(result);

        }
    }
}
