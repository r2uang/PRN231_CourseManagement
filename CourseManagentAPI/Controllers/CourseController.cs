using Microsoft.AspNetCore.Mvc;

namespace CourseManagmentAPI.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
