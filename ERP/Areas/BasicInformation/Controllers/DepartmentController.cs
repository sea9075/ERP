using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<Department> departmentList = (await _unitOfWork.Department.GetAllAsync()).ToList();
            return View(departmentList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Department department = await _unitOfWork.Department.GetAsync(u => u.DepartmentId == id);

            if (department != null)
            {
                return View(department);
            }
            else
            {
                department = new Department();
                return View(department);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.DepartmentId == 0)
                {
                    _unitOfWork.Department.Add(department);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Department.Update(department);
                    TempData["success"] = "修改成功";
                }

                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {
                if (department.DepartmentId == 0)
                {
                    TempData["error"] = "新增失敗";
                }
                else
                {
                    TempData["error"] = "修改失敗";
                }

                return View(department);
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

            Department departmentDeleted = await _unitOfWork.Department.GetAsync(u => u.DepartmentId == id);
            _unitOfWork.Department.Remove(departmentDeleted);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "刪除成功";
            return RedirectToAction("Index");
        }
    }
}
