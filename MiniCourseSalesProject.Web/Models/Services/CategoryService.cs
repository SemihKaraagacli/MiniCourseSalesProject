using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Dtos;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Models.Services
{
    public class CategoryService(HttpClient client)
    {
        public async Task<ServiceResult<Guid>> CreateCategory(CategoryCreateViewModel viewModel)
        {
            var adress = "/Admin/Category";

            var response = await client.PostAsJsonAsync(adress, viewModel);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<Guid>.Fail(problemDetails!.Detail!);
            }

            var responseData = await response.Content.ReadFromJsonAsync<Guid>();
            return ServiceResult<Guid>.Success(responseData);
        }

        public async Task<ServiceResult> UpdateCategory(CategoryUpdateViewModel viewModel)
        {
            var adress = $"/Admin/Category";

            var response = await client.PutAsJsonAsync(adress, viewModel);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteCategory(Guid id)
        {
            var adress = $"/Admin/Category/{id}";

            var response = await client.DeleteAsync(adress);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<CategoryResponse>>> AllCategory()
        {
            var adress = "/Admin/Category";

            var response = await client.GetAsync(adress);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<List<CategoryResponse>>.Fail(problemDetails!.Detail!);
            }

            var responseData = await response.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            return ServiceResult<List<CategoryResponse>>.Success(responseData);
        }
        public async Task<ServiceResult<CategoryUpdateViewModel>> ByIdCategory(Guid id)
        {
            var adress = $"/Admin/Category/{id}";

            var response = await client.GetAsync(adress);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<CategoryUpdateViewModel>.Fail(problemDetails!.Detail!);
            }

            var responseData = await response.Content.ReadFromJsonAsync<CategoryUpdateViewModel>();
            return ServiceResult<CategoryUpdateViewModel>.Success(responseData);
        }
    }
}
