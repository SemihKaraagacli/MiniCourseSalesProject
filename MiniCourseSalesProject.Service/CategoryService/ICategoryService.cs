using MiniCourseSalesProject.Service.CategoryService.Dtos;

namespace MiniCourseSalesProject.Service.CategoryService
{
    public interface ICategoryService
    {

        Task<ServiceResult<Guid>> CreateCategoryAsync(CategoryCreateRequest request);
        Task<ServiceResult> UpdateCategoryAsync(CategoryUpdateRequest request);
        Task<ServiceResult<CategoryDto>> GetCategoryByIdAsync(Guid categoryId);
        Task<ServiceResult<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ServiceResult> DeleteCategoryAsync(Guid categoryId);

    }
}