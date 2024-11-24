using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Dtos;

namespace MiniCourseSalesProject.Web.Models.Services
{
    public class PaymentService(HttpClient client)
    {
        public async Task<ServiceResult<PaymentResponse>> PaymentProcess(PaymentRequestViewModel viewModel)
        {
            var address = "/payment";
            var response = await client.PostAsJsonAsync(address, viewModel);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<PaymentResponse>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<PaymentResponse>();
            return ServiceResult<PaymentResponse>.Success(responseData);

        }
    }
}
