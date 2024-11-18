namespace MiniCourseSalesProject.Service.User.Dtos
{
    public record SignUpRequest(string UserName, string Email, string Password, decimal Wallet);
}
