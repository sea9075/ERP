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

        public async Task<IActionResult> Index()
        {
            List<Category> categoryList = (await _unitOfWork.Category.GetAllAsync()).ToList();
            return View(categoryList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Category category = await _unitOfWork.Category.GetAsync(u => u.CategoryId == id);

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
        public async Task<IActionResult> Upsert(Category category)
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

                await _unitOfWork.SaveAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
            }

            Category categoryDeleted = await _unitOfWork.Category.GetAsync(u => u.CategoryId == id);
            _unitOfWork.Category.Remove(categoryDeleted);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "刪除成功";
            return RedirectToAction("Index");
        }
    }
}
