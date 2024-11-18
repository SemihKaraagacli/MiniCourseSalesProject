using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service.CategoryService.Dtos;
using MiniCourseSalesProject.Service.CategoryService;
using MiniCourseSalesProject.Service.CourseService.Dtos;
using MiniCourseSalesProject.Service.CourseService;

namespace MiniCourseSalesProject.Api.Controllers
{
    public class AdminController(ICategoryService categoryService, ICourseService courseService) : CustomControllerBase
    {
        //Kategori ekleme silme düzenleme
        // ürün ekleme silme düzenleme
        // kullanıcılara role atama
        // sistemdeki tüm loglar

        //CATEGORY SIDE
        [HttpPost("Category")]
        public async Task<IActionResult> CategoryCreate(CategoryCreateRequest categoryCreateRequest)
        {
            var result = await categoryService.CreateCategoryAsync(categoryCreateRequest);
            return CreateObjectResult(result);
        }
        [HttpPut("Category")]
        public async Task<IActionResult> CategoryUpdate(CategoryUpdateRequest categoryUpdateRequest)
        {
            var result = await categoryService.UpdateCategoryAsync(categoryUpdateRequest);
            return CreateObjectResult(result);
        }
        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> CategoryDelete(Guid id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);
            return CreateObjectResult(result);
        }
        [HttpGet("Category")]
        public async Task<IActionResult> CategoryGet()
        {
            var result = await categoryService.GetAllCategoriesAsync();
            return CreateObjectResult(result);
        }
        [HttpGet("Category/{id}")]
        public async Task<IActionResult> CategoryGet(Guid id)
        {
            var result = await categoryService.GetCategoryByIdAsync(id);
            return CreateObjectResult(result);
        }



        //COURSE SIDE

        [HttpPost("Course")]
        public async Task<IActionResult> CourseCreate(CourseCreateRequest courseCreateRequest)
        {
            var result = await courseService.CreateCourseAsync(courseCreateRequest);
            return CreateObjectResult(result);
        }
        [HttpPut("Course")]
        public async Task<IActionResult> CourseUpdate(CourseUpdateRequest courseUpdateRequest)
        {
            var result = await courseService.UpdateCourseAsync(courseUpdateRequest);
            return CreateObjectResult(result);
        }
        [HttpDelete("Course/{id}")]
        public async Task<IActionResult> CourseDelete(Guid id)
        {
            var result = await courseService.DeleteCourseAsync(id);
            return CreateObjectResult(result);
        }
        [HttpGet("Course")]
        public async Task<IActionResult> CourseGet()
        {
            var result = await courseService.GetAllCoursesAsync();
            return CreateObjectResult(result);
        }
        [HttpGet("Course/{id}")]
        public async Task<IActionResult> CourseGet(Guid id)
        {
            var result = await courseService.GetCourseByIdAsync(id);
            return CreateObjectResult(result);
        }
    }
}
