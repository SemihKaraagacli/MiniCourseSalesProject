using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Dtos;

namespace MiniCourseSalesProject.Web.Models.Services
{
    public class UserService(HttpClient client)
    {
        public async Task<ServiceResult<List<UserResponse>>> GetAllUser()
        {
            var adress = "/Admin/AllUser";
            var response = await client.GetAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<List<UserResponse>>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<List<UserResponse>>();
            return ServiceResult<List<UserResponse>>.Success(responseData);
        }
        public async Task<ServiceResult> DeleteUser(Guid id)
        {
            var adress = $"/User/{id}";
            var response = await client.DeleteAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }
            return ServiceResult.Success();
        }
    }
}
