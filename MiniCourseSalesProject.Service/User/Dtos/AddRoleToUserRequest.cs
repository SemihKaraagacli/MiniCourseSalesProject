namespace MiniCourseSalesProject.Service.User.Dtos
{
    public record AddRoleToUserRequest(Guid userId, string RoleName);
}
