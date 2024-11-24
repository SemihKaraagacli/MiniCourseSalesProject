using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiniCourseSalesProject.Web.Models.Services;
using MiniCourseSalesProject.Web.Models.Validations;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class AuthController(AuthService authService, IValidator<SignInViewModel> signInValidator, IValidator<SignUpViewModel> signUpValidator) : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(SignInViewModel viewModel)
        {
            var validatorResult = await signInValidator.ValidateAsync(viewModel);
            if (!validatorResult.IsValid)
            {
                ModelState.Clear();
                foreach (var failure in validatorResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return View(viewModel);
            }

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

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignUpViewModel viewModel)
        {

            var validatorResult = await signUpValidator.ValidateAsync(viewModel);
            if (!validatorResult.IsValid)
            {
                ModelState.Clear();
                foreach (var failure in validatorResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return View(viewModel);
            }
            var response = await authService.SignUpAsync(viewModel);

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

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
