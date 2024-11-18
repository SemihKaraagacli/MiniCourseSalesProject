using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Service.OrderService.Dtos
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public List<Guid> CourseIds { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
}
