namespace MiniCourseSalesProject.Repository.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public AppUser User { get; set; }
        public Order? Order { get; set; }
    }
}