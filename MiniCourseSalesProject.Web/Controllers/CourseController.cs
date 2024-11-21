using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Services;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class CourseController(CourseService courseService, CategoryService categoryService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> FilterCourseByCategory(Guid? categoryId)
        {
            var categories = await categoryService.AllCategory();
            var model = new CourseByCategoryResponseViewModel
            {
                Categories = categories.Data,
                categoryId = categoryId
            };

            if (!categoryId.HasValue)
            {
                // Tüm kursları getir
                var courses = await courseService.AllCourse();
                model.Courses = courses.Data;
            }
            else
            {
                // Seçilen kategoriye ait kursları getir
                var courses = await courseService.GetCoursesByCategory(categoryId.Value);
                model.Courses = courses.Data;
            }

            return View(model);
        }
    }
}