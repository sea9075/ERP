using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<Supplier> supplierList = (await _unitOfWork.Supplier.GetAllAsync()).ToList();
            return View(supplierList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Supplier supplier = await _unitOfWork.Supplier.GetAsync(u => u.SupplierId == id);

            if (supplier != null)
            {
                return View(supplier);
            }
            else
            {
                supplier = new Supplier();
                return View(supplier);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.SupplierId == 0)
                {
                    _unitOfWork.Supplier.Add(supplier);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Supplier.Update(supplier);
                    TempData["success"] = "修改成功";
                }

                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {
                if (supplier.SupplierId == 0)
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

            Supplier supplierDeleted = await _unitOfWork.Supplier.GetAsync(u => u.SupplierId == id);
            _unitOfWork.Supplier.Remove(supplierDeleted);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "刪除成功";
            return RedirectToAction("Index");
        }
    }
}
