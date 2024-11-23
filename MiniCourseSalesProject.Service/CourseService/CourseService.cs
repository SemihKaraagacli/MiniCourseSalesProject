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
                CategoryName = newCourse.Category.Name,
                CreatedDate = newCourse.CreatedDate,
                UpdatedDate = newCourse.UpdatedDate,

            };
            return ServiceResult<Guid>.Success(courseToDto.Id, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<CourseDto>>> GetAllCoursesAsync()
        {
            var allCourse = await courseRepository.GetAllAsync();

            var courseToDto = new List<CourseDto>();

            foreach (var course in allCourse)
            {
                var category = await categoryRepository.GetByIdAsync(course.CategoryId);
                courseToDto.Add(new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    Price = course.Price,
                    CategoryName = category.Name,
                    CreatedDate = course.CreatedDate,
                    UpdatedDate = course.UpdatedDate,
                });
            }

            return ServiceResult<List<CourseDto>>.Success(courseToDto, HttpStatusCode.OK);

        }

        public async Task<ServiceResult<CourseDto>> GetCourseByIdAsync(Guid courseId)
        {
            var hasCourse = await courseRepository.GetByIdAsync(courseId);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Fail("Course not found.", HttpStatusCode.NotFound);
            }
            var category = categoryRepository.GetByIdAsync(hasCourse.CategoryId).Result;
            var courseDto = new CourseDto
            {
                Id = hasCourse.Id,
                Name = hasCourse.Name,
                Description = hasCourse.Description,
                Price = hasCourse.Price,
                CategoryName = category.Name,
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
            hasCourse.UpdatedDate = DateTime.UtcNow;
            hasCourse.CategoryId = request.CategoryId;

            courseRepository.Update(hasCourse);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<CourseDto>>> GetCoursesByCategoryAsync(Guid categoryId)
        {
            var courseByCategory = await courseRepository.GetCoursesByCategory(categoryId);
            if (courseByCategory is null)
            {
                return ServiceResult<List<CourseDto>>.Fail("Course not found.", HttpStatusCode.NotFound);
            }
            var courseToDto = new List<CourseDto>();
            foreach (var course in courseByCategory)
            {
                var category = await categoryRepository.GetByIdAsync(course.CategoryId);
                courseToDto.Add(new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    Price = course.Price,
                    CategoryName = category.Name,
                    CreatedDate = course.CreatedDate,
                    UpdatedDate = course.UpdatedDate,
                });
            }



            return ServiceResult<List<CourseDto>>.Success(courseToDto, HttpStatusCode.OK);
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


    }
}
