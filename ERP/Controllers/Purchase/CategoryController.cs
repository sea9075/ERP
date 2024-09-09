using ERP.Data;
using ERP.Models.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers.Purchase
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                Category category = new Category();
                return View(category);
            }
            else
            {
                Category category = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
                return View(category);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    _db.Categories.Add(category);
                    TempData["success"] = "新增類別成功";
                }
                else
                {
                    _db.Categories.Update(category);
                    TempData["success"] = "修改類別成功";
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                if (category.CategoryId == 0)
                {
                    TempData["error"] = "新增類別失敗";
                }
                else
                {
                    TempData["error"] = "修改類別失敗";
                }
                return View(category);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return Json(new {data = categoryList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var categoryDelete = _db.Categories.Find(id);

            if (categoryDelete == null)
            {
                return Json(new {success = false, message = "刪除失敗"});
            }

            _db.Categories.Remove(categoryDelete);
            _db.SaveChanges();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
