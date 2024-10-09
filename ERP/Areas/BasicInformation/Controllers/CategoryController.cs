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
            Category myCompany = _unitOfWork.Category.Get(u => u.CategoryId == id);

            if (myCompany != null)
            {
                return View(myCompany);
            }
            else
            {
                myCompany = new Category();
                return View(myCompany);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Category myCompany)
        {
            if (ModelState.IsValid)
            {
                if (myCompany.CategoryId == 0)
                {
                    _unitOfWork.Category.Add(myCompany);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Category.Update(myCompany);
                    TempData["success"] = "修改成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (myCompany.CategoryId == 0)
                {
                    TempData["error"] = "新增失敗";
                }
                else
                {
                    TempData["error"] = "修改失敗";
                }

                return View(myCompany);
            }
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category categoryDeleted = _unitOfWork.Category.Get(u => u.CategoryId == id);
            _unitOfWork.Category.Remove(categoryDeleted);
            TempData["success"] = "刪除成功";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
