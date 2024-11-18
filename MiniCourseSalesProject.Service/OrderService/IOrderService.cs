using MiniCourseSalesProject.Service.OrderService.Dtos;

namespace MiniCourseSalesProject.Service.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResult<Guid>> CreateOrderAsync(OrderCreateRequest request);
        Task<ServiceResult> UpdateOrderAsync(OrderUpdateRequest request);
        Task<ServiceResult<OrderDto>> GetOrderByIdAsync(Guid orderId);
        Task<ServiceResult<List<OrderDto>>> GetOrderAllAsync();
        Task<ServiceResult> DeleteOrderAsync(Guid orderId);
        Task<ServiceResult<List<OrderDto>>> GetOrdersByUserIdAsync(Guid userId);


        //Task<ServiceResult<Guid>> Create(OrderCreateRequest orderCreateRequest);
        //Task<ServiceResult> Delete(Guid id);
        //Task<ServiceResult<List<OrderDto>>> Get();
        //Task<ServiceResult<OrderDto>> Get(Guid id);
        //Task<ServiceResult> Update(OrderUpdateRequest orderUpdateRequest);
    }
}