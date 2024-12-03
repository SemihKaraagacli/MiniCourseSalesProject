using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniCourseSalesProject.Service;

namespace MiniCourseSalesProject.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
        private readonly ILogger<CustomControllerBase> _logger;

        public CustomControllerBase(ILogger<CustomControllerBase> logger, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomControllerBase>();
            //_logger = logger;
        }

        [NonAction]
        public IActionResult CreateObjectResult<T>(ServiceResult<T> result)
        {
            if (result.IsError)
            {
                //_logger.LogError("Error: {ErrorMessage}", result.Errors.First());
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                    Title = "An error occurred.",
                    Status = (int)result.Status,
                    Detail = result.Errors.First()
                };
                _logger.LogInformation("ProblemDetails: {@ProblemDetails}", problemDetails);
                return new ObjectResult(problemDetails)
                {
                    StatusCode = (int)result.Status,
                };

            }
            _logger.LogInformation("ProblemDetails: {@ProblemDetails}", result.Data);
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
                _logger.LogError("Error: {ErrorMessage}", result.Errors.First());
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                    Title = "An error occurred.",
                    Status = (int)result.Status,
                    Detail = result.Errors.First()
                };
                _logger.LogInformation("ProblemDetails: {@ProblemDetails}", problemDetails);
                return new ObjectResult(problemDetails)
                {
                    StatusCode = (int)result.Status,
                };
            }

            //_logger.LogInformation("Request succeeded with status {Status}", result.Status);
            return NoContent();
        }
    }

}
