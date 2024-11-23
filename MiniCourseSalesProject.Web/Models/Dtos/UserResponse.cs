namespace MiniCourseSalesProject.Web.Models.Dtos
{
    public record UserResponse(Guid Id, string Username, string Email, decimal Wallet, List<OrderCreateResponse> Orders);
}
