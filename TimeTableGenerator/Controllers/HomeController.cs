using Microsoft.AspNetCore.Mvc;

namespace TimeTableGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
