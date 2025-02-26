using LeNgocHaiMVC.Models;
using LeNgocHaiMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeNgocHaiMVC.Controllers
{
    [Authorize(Roles = "Staff")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService) // Inject Service vào Controller
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }
        // GET: Category
        public ActionResult Index()
        {

            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if(category == null) return NotFound();
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            _categoryService.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Edit/5
        public ActionResult Edit(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if(category == null) return NotFound();
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            _categoryService.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Delete/5
        public ActionResult Delete(short id)
        {
            try
            {
                if (!_categoryService.CanDeleteCategory(id))
                {
                    TempData["ErrorMessage"] = "Không thể xóa danh mục vì đã có bài viết sử dụng.";
                    return RedirectToAction(nameof(Index));
                }

                _categoryService.DeleteCategory(id);
                TempData["SuccessMessage"] = "Danh mục đã được xóa thành công.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi xóa danh mục: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
