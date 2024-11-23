using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Services;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class OrderController(OrderService orderService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Guid userId, Guid basketId)
        {
            OrderCreateViewModel viewModel = new OrderCreateViewModel();
            viewModel.UserId = userId;
            viewModel.BasketId = basketId;
            var response = await orderService.CreateOrder(viewModel);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("GetBasket ", "Basket", new { id = viewModel.UserId });
                }
            }
            return RedirectToAction("GetOrderById", "Order", new { orderId = response.Data });

        }

        [HttpGet("Order/GetOrderById/{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var response = await orderService.GetOrderById(orderId);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("GetBasket", "Basket");
                }
            }
            return View(response.Data);
        }

        [HttpGet("GetOrderByUserId")]
        public async Task<IActionResult> GetOrderByUserId(Guid userId)
        {
            var response = await orderService.GetOrderByUser(userId);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("GetBasket", "Basket");
                }
            }
            return View(response.Data);
        }

    }
}
