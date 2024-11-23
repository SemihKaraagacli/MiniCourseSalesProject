using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models.Services;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController(CategoryService categoryService, CourseService courseService, UserService userService, OrderService orderService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //CATEGORY
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateViewModel viewModel)
        {
            var response = await categoryService.CreateCategory(viewModel);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return RedirectToAction("AllCategory", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var response = await categoryService.ByIdCategory(id);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateViewModel viewModel)
        {
            var response = await categoryService.UpdateCategory(viewModel);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return RedirectToAction("AllCategory", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var response = await categoryService.DeleteCategory(id);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return RedirectToAction("AllCategory", "Admin");
        }
        public async Task<IActionResult> AllCategory()
        {
            var response = await categoryService.AllCategory();
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }

            return View(response.Data);
        }

        //COURSE
        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            var response = await categoryService.AllCategory();
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            var tupleModel = new CourseCreateTupleViewModel
            {
                Categories = response.Data.ToList(),
                Course = new CourseCreateRequest()
            };

            return View(tupleModel);

        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseCreateTupleViewModel request)
        {
            var response = await courseService.CreateCourse(request.Course);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return RedirectToAction("AllCourse", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCourse(Guid id)
        {
            var responseCourse = await courseService.GetById(id);

            var response = await categoryService.AllCategory();
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            var tupleModel = new CourseUpdateTupleViewModel
            {
                Categories = response.Data.ToList(),
                Course = new CourseUpdateRequest { Id = responseCourse.Data.Id }
            };

            return View(tupleModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCourse(CourseUpdateTupleViewModel viewModel)
        {
            var response = await courseService.UpdateCourse(viewModel.Course);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return RedirectToAction("AllCourse", "Admin");
        }
        [HttpPost("/{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var response = await courseService.DeleteCourse(id);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return RedirectToAction("AllCourse", "Admin");
        }
        public async Task<IActionResult> AllCourse()
        {
            var response = await courseService.AllCourse();
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return View(response.Data);
        }


        //ORDER
        [HttpGet]
        public async Task<IActionResult> AllOrder()
        {
            var response = await orderService.GetAllOrder();
            if (response.IsError)
            {
                foreach (var error in response!.Errors!)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return View();
        }

        [HttpPost("/order")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var response = await orderService.DeleteOrder(id);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("AllOrder", "Admin");
        }





        //USER
        [HttpGet]
        public async Task<IActionResult> AllUser()
        {
            var response = await userService.GetAllUser();
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return View(response.Data);
        }

        [HttpPost("/user")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await userService.DeleteUser(id);
            if (response.IsError)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();
                }
            }
            return RedirectToAction("AllUser", "Admin");
        }




    }
}
