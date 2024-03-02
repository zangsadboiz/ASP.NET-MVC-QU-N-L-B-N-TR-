using Microsoft.AspNetCore.Mvc;
using Teahub.Models;

namespace Teahub.Components
{
    [ViewComponent(Name = "Review")]
    public class ReviewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public ReviewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofPost = (from p in _context.Reviews
                              where (p.IsActive == true) && (p.Status == 1)
                              orderby p.ReviewID descending
                              select p).Take(3).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofPost));
        }
    }
}
