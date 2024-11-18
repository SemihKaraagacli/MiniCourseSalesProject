using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service;

namespace MiniCourseSalesProject.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
        [NonAction]
        public IActionResult CreateObjectResult<T>(ServiceResult<T> result)
        {
            if (result.IsError)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                    Title = "An error occurred.",
                    Status = (int)result.Status,
                    Detail = result.Errors.First()

                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = (int)result.Status,
                };

            }
            return new ObjectResult(result.Data)
            {
                StatusCode = (int)result.Status
            };
        }

        [NonAction]
        public IActionResult CreateObjectResult(ServiceResult result)
        {
            if (result.IsError)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                    Title = "An error occurred.",
                    Status = (int)result.Status,
                    Detail = result.Errors.First()

                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = (int)result.Status,
                };

            }
            return NoContent();
        }
    }
}
