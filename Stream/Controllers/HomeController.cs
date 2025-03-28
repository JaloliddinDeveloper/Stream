using Microsoft.AspNetCore.Mvc;

namespace Stream.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
