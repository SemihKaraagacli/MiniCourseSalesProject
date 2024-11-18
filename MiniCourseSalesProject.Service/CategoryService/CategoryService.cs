using AutoMapper;
using MiniCourseSalesProject.Repository.CategoryRepository;
using MiniCourseSalesProject.Repository.CourseRepository;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.CategoryService.Dtos;
using MiniCourseSalesProject.Service.CourseService.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.CategoryService
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork) : ICategoryService
    {
        public async Task<ServiceResult<Guid>> CreateCategoryAsync(CategoryCreateRequest request)
        {
            var hasCategory = await categoryRepository.WhereAsync(x => x.Name == request.Name);
            if (hasCategory.Any())
            {
                return ServiceResult<Guid>.Fail("Course already exists.", HttpStatusCode.BadGateway);
            }
            var newCategory = new Category
            {
                Name = request.Name,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.CommitAsync();
            var categoryToDto = new CategoryDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                CreatedDate = newCategory.CreatedDate,
                UpdatedDate = newCategory.UpdatedDate,
            };
            return ServiceResult<Guid>.Success(newCategory.Id, HttpStatusCode.OK);
        }

        public async Task<ServiceResult> DeleteCategoryAsync(Guid categoryId)
        {
            var hasCategory = await categoryRepository.GetByIdAsync(categoryId);
            if (hasCategory is null)
            {
                return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
            }
            categoryRepository.Delete(hasCategory);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var allCategory = await categoryRepository.GetAllAsync();
            var categoryToDto = allCategory.Select(x => new CategoryDto { Id = x.Id, Name = x.Name, CreatedDate = x.CreatedDate, UpdatedDate = x.UpdatedDate }).ToList();
            return ServiceResult<List<CategoryDto>>.Success(categoryToDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<CategoryDto>> GetCategoryByIdAsync(Guid categoryId)
        {
            var hasCategory = await categoryRepository.GetByIdAsync(categoryId);
            if (hasCategory is null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found.", HttpStatusCode.NotFound);
            }
            var categoryToDto = new CategoryDto
            {
                Id = hasCategory.Id,
                Name = hasCategory.Name,
                CreatedDate = hasCategory.CreatedDate,
                UpdatedDate = hasCategory.UpdatedDate,
            };
            return ServiceResult<CategoryDto>.Success(categoryToDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult> UpdateCategoryAsync(CategoryUpdateRequest request)
        {
            var hasCategory = await categoryRepository.GetByIdAsync(request.Id);
            if (hasCategory is null)
            {
                return ServiceResult<CategoryDto>.Fail("Course not found.", HttpStatusCode.NotFound);
            }
            hasCategory.Name = request.Name;
            hasCategory.UpdatedDate = DateTime.UtcNow;
            return ServiceResult.Success(HttpStatusCode.OK);
        }





        //public async Task<ServiceResult<Guid>> Create(CategoryCreateRequest categoryCreateRequest)
        //{
        //    var hasCategory = categoryRepository.WhereAsync(x => x.Name == categoryCreateRequest.Name);
        //    if (hasCategory)
        //    {
        //        return ServiceResult<Guid>.Fail("Category already exists.", HttpStatusCode.BadRequest);
        //    }
        //    var newCategory = new Category
        //    {
        //        Name = categoryCreateRequest.Name,
        //    };
        //    await categoryRepository.AddAsync(newCategory);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult<Guid>.Success(newCategory.Id, HttpStatusCode.OK);

        //}
        //public async Task<ServiceResult> Update(CategoryUpdateRequest categoryUpdateRequest)
        //{
        //    var hasCategory = await categoryRepository.GetAsync(categoryUpdateRequest.Id);
        //    if (hasCategory is null)
        //    {
        //        return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
        //    }
        //    hasCategory.Name = categoryUpdateRequest.Name;
        //    categoryRepository.UpdateAsync(hasCategory);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult.Success(HttpStatusCode.OK);

        //}
        //public async Task<ServiceResult> Delete(Guid id)
        //{
        //    var hasCategory = await categoryRepository.GetAsync(id);
        //    if (hasCategory is null)
        //    {
        //        return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
        //    }
        //    categoryRepository.DeleteAsync(hasCategory);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult.Success(HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult<List<CategoryDto>>> Get()
        //{
        //    var allCategory = await categoryRepository.GetAsync();
        //    var entityToDto = mapper.Map<List<CategoryDto>>(allCategory);
        //    return ServiceResult<List<CategoryDto>>.Success(entityToDto, HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult<CategoryDto>> Get(Guid id)
        //{
        //    var selectCategory = await categoryRepository.GetAsync(id);
        //    if (selectCategory is null)
        //    {
        //        return ServiceResult<CategoryDto>.Fail("Category not found.", HttpStatusCode.NotFound);
        //    }
        //    var entityToDto = mapper.Map<CategoryDto>(selectCategory);
        //    return ServiceResult<CategoryDto>.Success(entityToDto, HttpStatusCode.OK);
        //}

    }
}
