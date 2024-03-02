using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Teahub.Areas.Admin.Models;
using Teahub.Models;

namespace Teahub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.AdminUsers.OrderBy(m => m.UserID).ToList();
            return View(mnList);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.AdminUsers.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleUser = _context.AdminUsers.Find(id);
            if (deleUser == null)
            {
                return NotFound();
            }
            _context.AdminUsers.Remove(deleUser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var mnList = (from m in _context.AdminUsers
                          select new SelectListItem()
                          {
                              Text = m.UserName,
                              Value = m.UserID.ToString(),
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

        public IActionResult Create(AdminUser mn)
        {
            //Validate dữ liệu xem dữ liệu nhập vào đúng k
            if (ModelState.IsValid)
            {
                _context.AdminUsers.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }

        // GET: Edit User
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.AdminUsers.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Edit User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AdminUser user)
        {
            if (ModelState.IsValid)
            {
                _context.AdminUsers.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

    }
}
