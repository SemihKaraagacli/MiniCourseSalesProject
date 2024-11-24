using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service.Auth;
using MiniCourseSalesProject.Service.Auth.Dtos;

namespace MiniCourseSalesProject.Api.Controllers
{
    public class AuthController(IAuthService authService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
    {
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest signInRequest)
        {
            var result = await authService.SignInAsync(signInRequest);
            return CreateObjectResult(result);
        }

        [HttpPost("SignInClientCredential")]
        public async Task<IActionResult> SignInClientCredential(SignInClientCredentialRequest request)
        {
            var result = await authService.SignInClientCredentialAsync(request);
            return CreateObjectResult(result);
        }
    }
}
