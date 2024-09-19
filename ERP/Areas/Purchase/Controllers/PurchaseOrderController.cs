﻿using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class PurchaseOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<PurchaseOrder> PurchaseOrderList = _unitOfWork.PurchaseOrder.GetAll(includeProperties: "Supplier").ToList();
            return View(PurchaseOrderList);
        }

        public IActionResult Upsert(int? id)
        {
            PurchaseOrderVM purchaseOrderVM = new()
            {
                PurchaseDetailVM = new PurchaseDetailVM(),
                SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.SupplierId.ToString()
                }),
                PurchaseOrder = new PurchaseOrder()
                
            };

            if (id == null || id == 0)
            {
                return View(purchaseOrderVM);
            }
            else
            {
                purchaseOrderVM.PurchaseOrder = _unitOfWork.PurchaseOrder.Get(u => u.PurchaseOrderId == id);
                return View(purchaseOrderVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PurchaseOrderVM purchaseOrderVM)
        {
            if (ModelState.IsValid)
            {
                if (purchaseOrderVM.PurchaseOrder.PurchaseOrderId == 0)
                {
                    _unitOfWork.PurchaseOrder.Add(purchaseOrderVM.PurchaseOrder);
                }
                else
                {
                    _unitOfWork.PurchaseOrder.Update(purchaseOrderVM.PurchaseOrder);
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (purchaseOrderVM.PurchaseOrder.PurchaseOrderId == 0)
                {
                    TempData["error"] = "新增進貨單失敗";
                }
                else
                {
                    TempData["error"] = "修改進貨單失敗";
                }
                return View(purchaseOrderVM.PurchaseOrder);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<PurchaseOrder> inventoryList = _unitOfWork.PurchaseOrder.GetAll(includeProperties: "Supplier").ToList();
            return Json(new { data = inventoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var purchasingOrderDeleted = _unitOfWork.PurchaseOrder.Get(u => u.PurchaseOrderId == id);

            if (purchasingOrderDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.PurchaseOrder.Remove(purchasingOrderDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
