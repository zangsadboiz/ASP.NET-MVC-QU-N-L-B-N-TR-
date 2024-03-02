using Microsoft.AspNetCore.Mvc;
using Teahub.Models;

namespace Teahub.Components
{
    [ViewComponent(Name = "Product")]
    public class ProductComponent : ViewComponent
    {
        private readonly DataContext _context;
        public ProductComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofPost = (from p in _context.Products
                              where (p.IsActive == true) && (p.Status == 1)
                              orderby p.ProductID descending
                              select p).Take(3).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofPost));
        }
    }
}
