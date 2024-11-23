using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Service.OrderService.Dtos
{
    public class OrderCreateRequest
    {
        public Guid UserId { get; set; }
        public Guid BasketId { get; set; }
    }
}
