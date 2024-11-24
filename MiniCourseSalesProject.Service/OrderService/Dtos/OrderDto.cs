using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Service.BasketService.Dtos;

namespace MiniCourseSalesProject.Service.OrderService.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public List<BasketItemInCourseResponse> BasketItemInCourseResponses { get; set; }
        public decimal Wallet { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
