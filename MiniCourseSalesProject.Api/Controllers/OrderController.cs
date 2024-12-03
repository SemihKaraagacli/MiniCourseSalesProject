using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Service.BasketService;
using MiniCourseSalesProject.Service.BasketService.Dtos;
using MiniCourseSalesProject.Service.OrderService;
using MiniCourseSalesProject.Service.OrderService.Dtos;

namespace MiniCourseSalesProject.Api.Controllers
{
    [Authorize]
    public class OrderController(IOrderService orderService, IBasketService basketService, ILogger<CustomControllerBase> logger, ILoggerFactory loggerFactory) : CustomControllerBase(logger, loggerFactory)
    {
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateRequest orderCreateRequest)
        {
            var result = await orderService.CreateOrderAsync(orderCreateRequest);
            return CreateObjectResult(result);
        }
        [HttpDelete("/order/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await orderService.DeleteOrderAsync(id);
            return CreateObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await orderService.GetOrderByIdAsync(id);
            return CreateObjectResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await orderService.GetOrderAllAsync();
            return CreateObjectResult(result);
        }

        [HttpGet("GetOrderByUser/{id}")]
        public async Task<IActionResult> GetOrderByUser(Guid id)
        {
            var result = await orderService.GetOrdersByUserIdAsync(id);
            return CreateObjectResult(result);
        }





        [HttpDelete("/Basket/DeleteCourseFromBasketAsync/{UserId}/{BasketItemId}")]
        public async Task<IActionResult> DeleteCourseFromBasketAsync(Guid UserId, Guid BasketItemId)
        {
            var result = await basketService.DeleteCourseFromBasketAsync(UserId, BasketItemId);
            return CreateObjectResult(result);
        }
        [HttpPost("/Basket")]
        public async Task<IActionResult> CreateBasket(BasketCreateRequest request)
        {
            var result = await basketService.CreateBasketAsync(request);
            return CreateObjectResult(result);
        }
        [HttpGet("/Basket/{UserId}")]
        public async Task<IActionResult> GetAllBasket(Guid UserId)
        {
            var result = await basketService.GetBasketItemInCourseAsync(UserId);
            return CreateObjectResult(result);
        }
        [HttpDelete("/Basket/{id}")]
        public async Task<IActionResult> DeleteBasket(Guid id)
        {
            var result = await basketService.DeleteBasketAsync(id);
            return CreateObjectResult(result);
        }
    }
}
