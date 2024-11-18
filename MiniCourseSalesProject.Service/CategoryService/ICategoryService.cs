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





        //Task<ServiceResult<Guid>> Create(CategoryCreateRequest categoryCreateRequest);
        //Task<ServiceResult> Delete(Guid id);
        //Task<ServiceResult<List<CategoryDto>>> Get();
        //Task<ServiceResult<CategoryDto>> Get(Guid id);
        //Task<ServiceResult> Update(CategoryUpdateRequest categoryUpdateRequest);
    }
}