using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Teahub.Models;

namespace Teahub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly DataContext _context;

        public MenuController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.Menus.OrderBy(m => m.MenuID).ToList();
            return View(mnList);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Menus.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        // 
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Menus.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.Menus.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();
        }
        // xử lý dữ liệu khi người dùng gửi lên 1 request bằng phương thức post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Menu mn)
        {
            //Validate dữ liệu xem dữ liệu nhập vào đúng k
            if (ModelState.IsValid)
            {
                _context.Menus.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }

            var menu = _context.Menus.Find(id);
            if (menu == null)
            {
                return View();
            }

            var menuList = _context.Menus
                .Select(m => new SelectListItem
                {
                    Text = m.MenuName,
                    Value = m.MenuID.ToString()
                })
                .ToList();

            menuList.Insert(0, new SelectListItem
            {
                Text = "----Select----",
                Value = string.Empty
            });

            ViewBag.mnList = menuList;

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Menus.Update(menu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mnList = _context.Menus
                .Select(m => new SelectListItem
                {
                    Text = m.MenuName,
                    Value = m.MenuID.ToString()
                })
                .ToList();

            return View(menu);
        }

    }
}

