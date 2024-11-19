using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Web.Models;
using System.Diagnostics;

namespace MiniCourseSalesProject.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
