using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using MiniCourseSalesProject.Web.Models.Dtos;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Models.Services
{
    public class OrderService(HttpClient client)
    {
        //ORDER
        public async Task<ServiceResult> GetAllOrder()
        {
            var adress = "/Order";
            var response = await client.GetAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }
            return ServiceResult.Success();
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

        public async Task<ServiceResult<Guid>> CreateOrder(OrderCreateViewModel viewModel)
        {
            var address = "/order";
            var response = await client.PostAsJsonAsync(address, viewModel);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<Guid>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<Guid>();
            return ServiceResult<Guid>.Success(responseData);
        }

        public async Task<ServiceResult<OrderResponse>> GetOrderById(Guid orderId)
        {
            var adress = $"/order/{orderId}";
            var response = await client.GetAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<OrderResponse>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<OrderResponse>();
            return ServiceResult<OrderResponse>.Success(responseData);
        }

        public async Task<ServiceResult<List<OrderResponse>>> GetOrderByUser(Guid userId)
        {
            var adress = $"/order/GetOrderByUser/{userId}";
            var response = await client.GetAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<List<OrderResponse>>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
            return ServiceResult<List<OrderResponse>>.Success(responseData);
        }




        //BASKET
        public async Task<ServiceResult> AddToBasket(BasketCreateViewModel viewModel)
        {
            var adress = "/Basket";
            var response = await client.PostAsJsonAsync(adress, viewModel);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadAsStringAsync();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<CourseFromBasketItemResponse>>> GetBasket(Guid UserId)
        {
            var address = $"/Basket/{UserId}";
            var response = await client.GetAsync(address);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<List<CourseFromBasketItemResponse>>.Fail(problemDetails!.Detail!);
            }
            if (response.Content == null || response.Content.Headers.ContentLength == 0)
            {
                return ServiceResult<List<CourseFromBasketItemResponse>>.Success(null); // İçerik boş ise null dön
            }
            var responseData = await response.Content.ReadFromJsonAsync<List<CourseFromBasketItemResponse>>();
            return ServiceResult<List<CourseFromBasketItemResponse>>.Success(responseData);
        }

        public async Task<ServiceResult> DeleteToBasketItem(Guid UserId, Guid BasketItemId)
        {
            var address = $"/Basket/DeleteCourseFromBasketAsync/{UserId}/{BasketItemId}"; // API adresi
            var response = await client.DeleteAsync(address);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }
            return ServiceResult.Success();
        }

    }
}
