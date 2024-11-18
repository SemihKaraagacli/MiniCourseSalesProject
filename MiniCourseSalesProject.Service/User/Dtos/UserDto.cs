
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Service.OrderService.Dtos;

namespace MiniCourseSalesProject.Service.User.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<OrderDto> Orders { get; set; }
        public decimal Wallet { get; set; }
    }
}