using Microsoft.AspNetCore.Identity;

namespace MiniCourseSalesProject.Repository.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public decimal Wallet { get; set; }
        public List<Order> Orders { get; set; }
        public List<Basket> Baskets { get; set; }
    }
}
