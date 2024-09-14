using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Supplier> supplierList = _unitOfWork.Supplier.GetAll().ToList();
            return View(supplierList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                Supplier supplier = new Supplier();
                return View(supplier);
            }
            else
            {
                Supplier supplier = _unitOfWork.Supplier.Get(u => u.SupplierId == id);
                return View(supplier);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.SupplierId == 0)
                {
                    _unitOfWork.Supplier.Add(supplier);
                    TempData["success"] = "新增類別成功";
                }
                else
                {
                    _unitOfWork.Supplier.Update(supplier);
                    TempData["success"] = "修改類別成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (supplier.SupplierId == 0)
                {
                    TempData["error"] = "新增類別失敗";
                }
                else
                {
                    TempData["error"] = "修改類別失敗";
                }
                return View(supplier);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<Supplier> supplierList = _unitOfWork.Supplier.GetAll().ToList();
            return Json(new { data = supplierList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var supplierDeleted = _unitOfWork.Supplier.Get(u => u.SupplierId == id);

            if (supplierDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Supplier.Remove(supplierDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
