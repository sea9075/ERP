using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
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
            List<Supplier> supllierList = _unitOfWork.Supplier.GetAll().ToList();
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Supplier supplier = _unitOfWork.Supplier.Get(u => u.SupplierId == id);

            if (supplier == null)
            {
                return View(new Supplier());
            }
            else
            {
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
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Supplier.Update(supplier);
                    TempData["success"] = "修改成功";
                }

                _unitOfWork.Save();
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

                return View(supplier);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Supplier? supplier = _unitOfWork.Supplier.Get(u => u.SupplierId == id);
            if (supplier != null)
            {
                _unitOfWork.Supplier.Reomve(supplier);
                TempData["success"] = "刪除成功";
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
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

            if (supplierDeleted != null)
            {
                _unitOfWork.Supplier.Reomve(supplierDeleted);
                _unitOfWork.Save();
                return Json(new { success = true , message = "刪除成功"});
            }
            else
            {
                return Json(new { success = false, message = "刪除失敗" });
            }
        }

        #endregion
    }
}
