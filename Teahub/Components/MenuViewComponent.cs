using Microsoft.AspNetCore.Mvc;
using Teahub.Models;

namespace Teahub.Components
{
    [ViewComponent(Name = "MenuView")]
    public class MenuViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public MenuViewComponent(DataContext context)   
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() // Sửa chính tả của 'InvokeAsyns' thành 'InvokeAsync'
        {
            var listOfMenu = (from m in _context.Menus
                              where (m.IsActive == true) && (m.Position == 1)
                              select m).ToList();
            return View("Default", listOfMenu); // await không cần thiết ở đây vì đã trả về kết quả trực tiếp
        }
    }
}
