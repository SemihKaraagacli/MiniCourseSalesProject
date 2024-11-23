using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Service.OrderService.Dtos
{
    public class OrderUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
    }
}
