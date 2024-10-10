using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
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
            Category category = _unitOfWork.Category.Get(u => u.CategoryId == id);

            if (category != null)
            {
                return View(category);
            }
            else
            {
                category = new Category();
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
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                    TempData["success"] = "修改成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (category.CategoryId == 0)
                {
                    TempData["error"] = "新增失敗";
                }
                else
                {
                    TempData["error"] = "修改失敗";
                }

                return View(category);
            }
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
            }

            Category categoryDeleted = _unitOfWork.Category.Get(u => u.CategoryId == id);
            _unitOfWork.Category.Remove(categoryDeleted);
            TempData["success"] = "刪除成功";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
