namespace MiniCourseSalesProject.Service.BasketService.Dtos
{
    public class DeleteBasketItem
    {
        public Guid UserId { get; set; }
        public Guid BasketId { get; set; }
    }
}
