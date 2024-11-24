using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Services;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class HomeController(CourseService courseService, UserService userService) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdminRole(Guid UserId)
        {
            var response = await userService.AddAdminRole(UserId);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("SignOut", "Auth");
        }
    }
}
