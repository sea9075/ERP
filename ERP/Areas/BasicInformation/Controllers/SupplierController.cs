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

        public IActionResult Index()
        {
            List<Supplier> supplierList = _unitOfWork.Supplier.GetAll().ToList();
            return View(supplierList);
        }

        public IActionResult Upsert(int? id)
        {
            Supplier supplier = _unitOfWork.Supplier.Get(u => u.SupplierId == id);

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
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
            }

            Supplier supplierDeleted = _unitOfWork.Supplier.Get(u => u.SupplierId == id);
            _unitOfWork.Supplier.Remove(supplierDeleted);
            TempData["success"] = "刪除成功";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
