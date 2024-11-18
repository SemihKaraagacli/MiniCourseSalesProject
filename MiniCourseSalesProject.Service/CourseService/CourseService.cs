using AutoMapper;
using MiniCourseSalesProject.Repository.CategoryRepository;
using MiniCourseSalesProject.Repository.CourseRepository;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.CategoryService;
using MiniCourseSalesProject.Service.CategoryService.Dtos;
using MiniCourseSalesProject.Service.CourseService.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.CourseService
{
    public class CourseService(ICourseRepository courseRepository, ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork) : ICourseService
    {
        public async Task<ServiceResult<Guid>> CreateCourseAsync(CourseCreateRequest request)
        {
            var hasCourse = await courseRepository.WhereAsync(x => x.Name == request.Name);
            if (hasCourse.Any())
            {
                return ServiceResult<Guid>.Fail("Course already exists.", HttpStatusCode.BadGateway);
            }
            var hasCategory = await categoryRepository.GetByIdAsync(request.CategoryId);
            if (hasCategory is null)
            {
                return ServiceResult<Guid>.Fail("Category not found.", HttpStatusCode.NotFound);
            }
            var newCourse = new Course
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CategoryId = request.CategoryId,
            };
            await courseRepository.AddAsync(newCourse);
            await unitOfWork.CommitAsync();
            var courseToDto = new CourseDto
            {
                Id = newCourse.Id,
                Name = newCourse.Name,
                Description = newCourse.Description,
                Price = newCourse.Price,
                Quantity = newCourse.Quantity,
                CategoryName = newCourse.Name,
                CreatedDate = newCourse.CreatedDate,
                UpdatedDate = newCourse.UpdatedDate,

            };
            return ServiceResult<Guid>.Success(courseToDto.Id, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<CourseDto>>> GetAllCoursesAsync()
        {
            var allCourse = await courseRepository.GetAllAsync();
            var courseToDto = allCourse.Select(x =>
            {
                var category = categoryRepository.GetByIdAsync(x.CategoryId).Result;
                return new CourseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CategoryName = x.Name,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,

                };
            }).ToList();
            return ServiceResult<List<CourseDto>>.Success(courseToDto, HttpStatusCode.OK);

        }

        public async Task<ServiceResult<CourseDto>> GetCourseByIdAsync(Guid courseId)
        {
            var hasCourse = await courseRepository.GetByIdAsync(courseId);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Fail("Course not found.", HttpStatusCode.NotFound);
            }
            var courseDto = new CourseDto
            {
                Id = hasCourse.Id,
                Name = hasCourse.Name,
                Description = hasCourse.Description,
                Price = hasCourse.Price,
                Quantity = hasCourse.Quantity,
                CategoryName = hasCourse.Name,
                CreatedDate = hasCourse.CreatedDate,
                UpdatedDate = hasCourse.UpdatedDate,
            };
            return ServiceResult<CourseDto>.Success(courseDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult> UpdateCourseAsync(CourseUpdateRequest request)
        {
            var hasCourse = await courseRepository.GetByIdAsync(request.Id);
            if (hasCourse is null)
            {
                return ServiceResult.Fail("Course not found.", HttpStatusCode.NotFound);
            }
            hasCourse.Name = request.Name;
            hasCourse.Description = request.Description;
            hasCourse.Price = request.Price;
            hasCourse.Quantity = request.Quantity;
            hasCourse.UpdatedDate = DateTime.UtcNow;
            hasCourse.CategoryId = request.CategoryId;

            courseRepository.Update(hasCourse);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }

        public Task<ServiceResult<List<CourseDto>>> GetCoursesByCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteCourseAsync(Guid courseId)
        {
            var hasCourse = await courseRepository.GetByIdAsync(courseId);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Fail("Course not found.", HttpStatusCode.NotFound);
            }
            courseRepository.Delete(hasCourse);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }



        //public async Task<ServiceResult<Guid>> Create(CourseCreateRequest courseCreateRequest)
        //{
        //    var hasCourse = courseRepository.Where(x => x.Name == courseCreateRequest.Name).Any();
        //    if (hasCourse)
        //    {
        //        return ServiceResult<Guid>.Fail("Course already exists.", HttpStatusCode.BadRequest);
        //    }
        //    var hasCategory = await categoryRepository.GetAsync(courseCreateRequest.CategoryId);
        //    var newCourse = new Course
        //    {
        //        Price = courseCreateRequest.Amount,
        //        Description = courseCreateRequest.Description,
        //        Name = courseCreateRequest.Name,
        //        Orders = new List<Order>(),
        //        CategoryId = hasCategory.Id
        //    };
        //    courseRepository.CreateAsync(newCourse);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult<Guid>.Success(newCourse.Id, HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult> Update(Guid Id, CourseUpdateRequest courseUpdateRequest)
        //{
        //    var hasCategory = await courseRepository.GetAsync(Id);
        //    if (hasCategory is null)
        //    {
        //        return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
        //    }

        //    hasCategory.Name = courseUpdateRequest.Name;
        //    hasCategory.Description = courseUpdateRequest.Description;
        //    hasCategory.CategoryId = courseUpdateRequest.CategoryId;
        //    hasCategory.Price = courseUpdateRequest.Amount;

        //    courseRepository.UpdateAsync(hasCategory);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult.Success(HttpStatusCode.OK);

        //}
        //public async Task<ServiceResult> Delete(Guid id)
        //{
        //    var hasCategory = await courseRepository.GetAsync(id);
        //    if (hasCategory is null)
        //    {
        //        return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
        //    }
        //    courseRepository.DeleteAsync(hasCategory);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult.Success(HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult<List<CourseDto>>> Get()
        //{
        //    var AllCourse = await courseRepository.GetAsync();
        //    var entityToDto = mapper.Map<List<CourseDto>>(AllCourse);
        //    return ServiceResult<List<CourseDto>>.Success(entityToDto, HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult<CourseDto>> Get(Guid id)
        //{
        //    var selectCategory = await courseRepository.GetAsync(id);
        //    if (selectCategory is null)
        //    {
        //        return ServiceResult<CourseDto>.Fail("Category not found.", HttpStatusCode.NotFound);
        //    }
        //    var entityToDto = mapper.Map<CourseDto>(selectCategory);
        //    return ServiceResult<CourseDto>.Success(entityToDto, HttpStatusCode.OK);
        //}

    }
}
