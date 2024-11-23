using MiniCourseSalesProject.Service.CourseService.Dtos;

namespace MiniCourseSalesProject.Service.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResult<Guid>> CreateCourseAsync(CourseCreateRequest request);
        Task<ServiceResult> UpdateCourseAsync(CourseUpdateRequest request);
        Task<ServiceResult<CourseDto>> GetCourseByIdAsync(Guid courseId);
        Task<ServiceResult<List<CourseDto>>> GetAllCoursesAsync();
        Task<ServiceResult> DeleteCourseAsync(Guid courseId);
        Task<ServiceResult<List<CourseDto>>> GetCoursesByCategoryAsync(Guid categoryId);

    }
}