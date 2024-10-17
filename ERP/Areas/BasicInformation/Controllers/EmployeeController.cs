using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using ERP.Models.BasicInformation.BasicInformationVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IPasswordHasher<Employee> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> employeeList = (await _unitOfWork.Employee.GetAllAsync(includeProperties: "Department")).ToList();
            return View(employeeList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            EmployeeVM employeeVM = new()
            {
                DepartmentList = (await _unitOfWork.Department.GetAllAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.DepartmentId.ToString()
                }),
                Employee = new Employee()
            };

            if (id == null || id == 0)
            {
                return View(employeeVM);
            }
            else
            {
                employeeVM.Employee = await _unitOfWork.Employee.GetAsync(u => u.EmployeeId ==  id);
                return View(employeeVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(EmployeeVM employeeVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (employeeVM.Employee.Password != null)
                {
                    // 登入帳號密碼
                    // 加密密碼
                    var hashPassword = _passwordHasher.HashPassword(employeeVM.Employee, employeeVM.Employee.Password);
                    employeeVM.Employee.Password = hashPassword;
                }

                // 新增圖片
                // 取得 wwwroot 路徑
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // 判斷是否有傳入圖片
                if (file != null)
                {
                    // 將圖片名稱使用 GUID 代替
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // 取得放置圖片位置
                    string employeeImagePath = Path.Combine(wwwRootPath, @"images\employee");

                    // 判斷是否存在舊圖片
                    if (!string.IsNullOrEmpty(employeeVM.Employee.Image))
                    {
                        // 取得舊圖片的檔案位置
                        var oldImagePath = Path.Combine(wwwRootPath, employeeVM.Employee.Image.TrimStart('\\'));

                        // 如果舊圖片位置有存在檔案
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            // 刪除舊圖片
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // 將上傳的圖片存入指定位置
                    using (var fileStream = new FileStream(Path.Combine(employeeImagePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // 將檔案位置存入資料表
                    employeeVM.Employee.Image = @"\images\employee\" + fileName;
                }

                if (employeeVM.Employee.EmployeeId == 0)
                {
                    _unitOfWork.Employee.Add(employeeVM.Employee);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Employee.Update(employeeVM.Employee);
                    TempData["success"] = "修改成功";
                }

                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {
                if (employeeVM.Employee.EmployeeId == 0)
                {
                    TempData["error"] = "新增失敗";
                }
                else
                {
                    TempData["error"] = "修改失敗";
                }

                return RedirectToAction("Index");
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

            Employee employeeDeleted = await _unitOfWork.Employee.GetAsync(u => u.EmployeeId == id);

            // 刪除圖片
            // 取得舊圖片位置
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, employeeDeleted.Image.TrimStart('\\'));

            // 如果舊圖片位置有存在檔案
            if (System.IO.File.Exists(oldImagePath))
            {
                // 刪除舊圖片
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Employee.Remove(employeeDeleted);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "刪除成功";
            return RedirectToAction("Index");
        }
    }
}
