using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Teahub.Models;
using Teahub.Utilities;

namespace Teahub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");


            var prdList = _context.Products.OrderBy(m => m.ProductID).ToList();
            return View(prdList);
        }


        //Hiển thị Trang Thêm mới  post
        public IActionResult Create()
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var prdList = (from m in _context.Menus
                            select new SelectListItem()
                            {
                                Text = m.MenuName,
                                Value = m.MenuID.ToString(),
                            }).ToList();
            prdList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.prdList = prdList;
            return View();
        }
        // xử lý dữ liệu khi người dùng gửi lên 1 request bằng phương thức post
        [HttpPost]

        public IActionResult Create(Product product)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            //Validate dữ liệu xem dữ liệu nhập vào đúng k
            if (ModelState.IsValid)
            {
                _context.Add(product);
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
            var post = _context.Products.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            var prdList = (from m in _context.Products
                            select new SelectListItem()
                            {
                                Text = m.ProductName,
                                Value = m.ProductID.ToString(),
                            }).ToList();
            prdList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.postList = prdList;
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
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
            var mn = _context.Products.Find(id);
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

            var deleprd = _context.Products.Find(id);
            if (deleprd == null)
            {
                return NotFound();
            }
            _context.Products.Remove(deleprd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}