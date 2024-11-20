using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Services;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class HomeController(CourseService courseService) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
