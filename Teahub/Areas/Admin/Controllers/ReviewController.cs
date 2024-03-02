using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Teahub.Models;
using Teahub.Utilities;

namespace Teahub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly DataContext _context;
        public ReviewController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");


            var prdList = _context.Reviews.OrderBy(m => m.ReviewID).ToList();
            return View(prdList);
        }


        //Hiển thị Trang Thêm mới  post
        public IActionResult Create()
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var rvList = (from m in _context.Menus
                           select new SelectListItem()
                           {
                               Text = m.MenuName,
                               Value = m.MenuID.ToString(),
                           }).ToList();
            rvList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.rvList = rvList;
            return View();
        }
        // xử lý dữ liệu khi người dùng gửi lên 1 request bằng phương thức post
        [HttpPost]

        public IActionResult Create(Review review)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            //Validate dữ liệu xem dữ liệu nhập vào đúng k
            if (ModelState.IsValid)
            {
                _context.Add(review);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(long? id)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            var rvList = (from m in _context.Reviews
                           select new SelectListItem()
                           {
                               Text = m.Title,
                               Value = m.ReviewID.ToString(),
                           }).ToList();
            rvList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.rvList = rvList;
            return View(review);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Review review)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _context.Reviews.Update(review);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }
        // Hiển thị trang xóa 1 bài viết
        public IActionResult Delete(long? id)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Reviews.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        // 
        [HttpPost]
        public IActionResult Delete(long id)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var deleprd = _context.Reviews.Find(id);
            if (deleprd == null)
            {
                return NotFound();
            }
            _context.Reviews.Remove(deleprd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
