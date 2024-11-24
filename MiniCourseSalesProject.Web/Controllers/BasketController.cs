using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Services;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class BasketController(OrderService orderService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateBasket(BasketCreateViewModel viewModel)
        {
            var response = await orderService.AddToBasket(viewModel);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("FilterCourseByCategory", "Course");
                }
            }
            return RedirectToAction("GetBasket", "Basket", new { id = viewModel.UserId });
        }

        [HttpGet("/Basket/GetBasket/{UserId}")]
        public async Task<IActionResult> GetBasket(Guid UserId)
        {
            var response = await orderService.GetBasket(UserId);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return View(response.Data);
        }

        [HttpPost("/Basket/DeleteBasketItemFromBasket")]
        public async Task<IActionResult> DeleteBasketItemFromBasket(Guid UserId, Guid BasketItemId)
        {
            var response = await orderService.DeleteToBasketItem(UserId, BasketItemId);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("FilterCourseByCategory", "Course");
                }
            }
            return RedirectToAction("GetBasket", "Basket", new { id = UserId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBasket(Guid BasketId)
        {
            var response = await orderService.DeleteToBasket(BasketId);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("FilterCourseByCategory", "Course");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
