using Microsoft.AspNetCore.Mvc;
using Teahub.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Teahub.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            var blogPosts = _context.Posts.ToList(); 
            return View(blogPosts); 
        }
        [Route("/list-{slug}-{id:int}.html", Name = "List")]
        public IActionResult List(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var list = _context.PostMenus.Where(m => (m.MenuID == id) && (m.IsActive == true)).Take(6).ToList();
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public BlogController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
    }
}
