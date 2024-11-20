using Microsoft.AspNetCore.Mvc;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
