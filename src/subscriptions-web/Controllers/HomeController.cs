using Microsoft.AspNetCore.Mvc;

namespace Esfa.Recruit.Subscriptions.Web.Controllers
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