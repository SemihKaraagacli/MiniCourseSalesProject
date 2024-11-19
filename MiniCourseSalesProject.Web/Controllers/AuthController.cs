using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class AuthController(AuthService authService) : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(SignInViewModel viewModel)
        {
            var response = await authService.SignInAsync(viewModel);

            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View();
            }


            return RedirectToAction("Index", "Home");
        }
    }
}
