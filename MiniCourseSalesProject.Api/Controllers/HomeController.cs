using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service.PaymentService;
using MiniCourseSalesProject.Service.PaymentService.Dtos;

namespace MiniCourseSalesProject.Api.Controllers
{
    public class HomeController(PaymentService paymentService) : CustomControllerBase
    {
        //Giriş sayfası
        //Ürünleri görüntüleme
        //sipariş alma

        [HttpPost]
        public async Task<IActionResult> ProcessPaymentAsync(PaymentCreateRequest request)
        {
            var result = await paymentService.ProcessPaymentAsync(request);
            return CreateObjectResult(result);

        }
    }
}
