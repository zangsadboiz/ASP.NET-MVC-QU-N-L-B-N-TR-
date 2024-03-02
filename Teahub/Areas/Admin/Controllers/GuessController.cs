using Microsoft.AspNetCore.Mvc;

namespace Teahub.Areas.Admin.Controllers
{
    public class GuessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
