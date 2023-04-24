using Microsoft.AspNetCore.Mvc;

namespace DysonDigitalDashboard.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
