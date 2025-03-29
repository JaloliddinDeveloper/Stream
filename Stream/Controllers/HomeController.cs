using Microsoft.AspNetCore.Mvc;
using Stream.Models.Foundations.Videos;
using System.Threading.Tasks;

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
