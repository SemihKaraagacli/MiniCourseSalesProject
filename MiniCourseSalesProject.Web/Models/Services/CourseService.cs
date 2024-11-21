using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Dtos;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Models.Services
{
    public class CourseService(HttpClient client)
    {

        public async Task<ServiceResult<List<CourseResponse>>> AllCourse()
        {
            var address = "/Admin/Course";


            var response = await client.GetAsync(address);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<List<CourseResponse>>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<List<CourseResponse>>();
            return ServiceResult<List<CourseResponse>>.Success(responseData);

        }
        public async Task<ServiceResult<CourseResponse>> GetById(Guid Id)
        {
            var address = $"/Admin/Course/{Id}";


            var response = await client.GetAsync(address);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<CourseResponse>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<CourseResponse>();
            return ServiceResult<CourseResponse>.Success(responseData);

        }

        public async Task<ServiceResult<Guid>> CreateCourse(CourseCreateRequest request)
        {
            var address = "/Admin/Course";

            var response = await client.PostAsJsonAsync(address, request);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<Guid>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<Guid>();
            return ServiceResult<Guid>.Success(responseData);
        }

        public async Task<ServiceResult> UpdateCourse(CourseUpdateRequest request)
        {
            var address = "/Admin/Course";

            var response = await client.PutAsJsonAsync(address, request);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadAsStringAsync();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteCourse(Guid Id)
        {
            var adress = $"/Admin/Course/{Id}";

            var response = await client.DeleteAsync(adress);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return ServiceResult.Success();
        }
        public async Task<ServiceResult<List<CourseResponse>>> GetCoursesByCategory(Guid? categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                var address = "/Admin/Course";


                var responseAll = await client.GetAsync(address);
                if (!responseAll.IsSuccessStatusCode)
                {
                    var problemDetails = await responseAll.Content.ReadFromJsonAsync<ProblemDetails>();
                    return ServiceResult<List<CourseResponse>>.Fail(problemDetails!.Detail!);
                }
                var responseAllData = await responseAll.Content.ReadFromJsonAsync<List<CourseResponse>>();
                return ServiceResult<List<CourseResponse>>.Success(responseAllData);
            }
            var adress = $"/course/GetCoursesByCategoryAsync/{categoryId}";
            var response = await client.GetAsync(adress);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult<List<CourseResponse>>.Fail(problemDetails!.Detail!);
            }
            var responseData = await response.Content.ReadFromJsonAsync<List<CourseResponse>>();
            return ServiceResult<List<CourseResponse>>.Success(responseData);
        }
    }
}
