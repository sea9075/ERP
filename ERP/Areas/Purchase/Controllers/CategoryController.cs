using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
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
                Category category = _unitOfWork.Category.Get(u => u.CategoryId == id);
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
                    _unitOfWork.Category.Add(category);
                    TempData["success"] = "新增類別成功";
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                    TempData["success"] = "修改類別成功";
                }

                _unitOfWork.Save();
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

            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var categoryDelete = _unitOfWork.Category.Get(u => u.CategoryId == id);

            if (categoryDelete == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Category.Remove(categoryDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
