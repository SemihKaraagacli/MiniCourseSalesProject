using MiniCourseSalesProject.Service.OrderService.Dtos;

namespace MiniCourseSalesProject.Service.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResult<Guid>> CreateOrderAsync(OrderCreateRequest request);
        Task<ServiceResult<OrderDto>> GetOrderByIdAsync(Guid orderId);
        Task<ServiceResult<List<OrderDto>>> GetOrderAllAsync();
        Task<ServiceResult> DeleteOrderAsync(Guid orderId);
        Task<ServiceResult<List<OrderDto>>> GetOrdersByUserIdAsync(Guid userId);

    }
}