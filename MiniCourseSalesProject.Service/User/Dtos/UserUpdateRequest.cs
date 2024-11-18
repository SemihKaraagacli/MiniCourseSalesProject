namespace MiniCourseSalesProject.Service.User.Dtos
{
    public record UserUpdateRequest(Guid Id, string UserName, string Email, decimal Wallet);
}
