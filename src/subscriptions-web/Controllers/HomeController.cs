using Microsoft.AspNetCore.Mvc;

namespace subscriptions_web.Controllers
{
    // [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}