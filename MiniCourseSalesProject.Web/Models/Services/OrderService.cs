using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Dtos;

namespace MiniCourseSalesProject.Web.Models.Services
{
    public class OrderService(HttpClient client)
    {
        public async Task<ServiceResult<List<OrderResponse>>> GetAllOrder()
        {
            var adress = "/Order";
            var response = await client.GetAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<List<OrderResponse>>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
            return ServiceResult<List<OrderResponse>>.Success(responseData);
        }

        public async Task<ServiceResult> DeleteOrder(Guid id)
        {
            var adress = $"/order/{id}";
            var response = await client.DeleteAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadAsStringAsync();
            return ServiceResult.Success();
        }
    }
}
