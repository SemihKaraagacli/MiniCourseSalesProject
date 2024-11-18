using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service.OrderService;
using MiniCourseSalesProject.Service.OrderService.Dtos;

namespace MiniCourseSalesProject.Api.Controllers
{
    public class OrderController(IOrderService orderService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateRequest orderCreateRequest)
        {
            var result = await orderService.CreateOrderAsync(orderCreateRequest);
            return CreateObjectResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(OrderUpdateRequest orderUpdateRequest)
        {
            var result = await orderService.UpdateOrderAsync(orderUpdateRequest);
            return CreateObjectResult(result);
        }
        [HttpDelete]
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
    }
}
