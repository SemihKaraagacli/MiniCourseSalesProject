using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Dtos;
using MiniCourseSalesProject.Web.Models.Services;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class PaymentController(PaymentService paymentService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> PaymentProcess(PaymentRequestViewModel viewModel)
        {
            var response = await paymentService.PaymentProcess(viewModel);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("FilterCourseByCategory", "Course");
                }
            }
            return View(response.Data);
        }
    }
}
