namespace MiniCourseSalesProject.Repository.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? BasketId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }

        // İlişkiler
        public Basket? Basket { get; set; }
        public AppUser User { get; set; }
        public Payment Payment { get; set; }
    }
}
