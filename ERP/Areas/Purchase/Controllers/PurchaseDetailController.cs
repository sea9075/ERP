using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class PurchaseDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Create(PurchaseDetailVM purchaseDetailVM)
        {
            if (ModelState.IsValid)
            {
                purchaseDetailVM.PurchaseDetail.SubTotal = purchaseDetailVM.PurchaseDetail.Cost * purchaseDetailVM.PurchaseDetail.Quantity;
                _unitOfWork.PurchaseDetail.Add(purchaseDetailVM.PurchaseDetail);
                _unitOfWork.Save();


                // 加入商品流向
                // --------------------
                ProductFlow productFlow = new ProductFlow();
                productFlow.From = "進貨單";
                productFlow.To = "庫存";
                productFlow.Action = "進貨";
                productFlow.Quantity = purchaseDetailVM.PurchaseDetail.Quantity;
                productFlow.ProductId = purchaseDetailVM.PurchaseDetail.ProductId;
                productFlow.Timeset = purchaseDetailVM.PurchaseDetail.Timeset;
                _unitOfWork.ProductFlow.Add(productFlow);
                _unitOfWork.Save();
                // --------------------


                // 加入庫存
                // --------------------
                Inventory inventory = _unitOfWork.Inventory.Get(u => u.ProductId == purchaseDetailVM.PurchaseDetail.ProductId);

                if (inventory == null)
                {
                    TempData["error"] = "新增採購單明細失敗，請新增商品庫存";
                    return RedirectToAction("Upsert", "PurchaseOrder", new { id = purchaseDetailVM.PurchaseDetail.PurchaseOrderId });
                }
                else 
                {
                    inventory.Quantity += purchaseDetailVM.PurchaseDetail.Quantity;
                    _unitOfWork.Inventory.Update(inventory);
                    _unitOfWork.Save();
                }
                // --------------------
                TempData["success"] = "新增商品明細成功";
                return RedirectToAction("Upsert", "PurchaseOrder", new { id = purchaseDetailVM.PurchaseDetail.PurchaseOrderId });
            }

            TempData["error"] = "新增商品明細失敗";
            return RedirectToAction("Upsert", "PurchaseOrder", new {id = purchaseDetailVM.PurchaseDetail.PurchaseOrderId});
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }

            PurchaseDetail purchaseDetailDelete = _unitOfWork.PurchaseDetail.Get(u => u.PurchaseDetailId == id);

            // 減少庫存
            Inventory inventory = _unitOfWork.Inventory.Get(u => u.ProductId == purchaseDetailDelete.ProductId);
            inventory.Quantity -= purchaseDetailDelete.Quantity;

            // 刪除流向
            ProductFlow productFlowDeleted = _unitOfWork.ProductFlow.Get(u => u.Timeset == purchaseDetailDelete.Timeset && u.ProductId == purchaseDetailDelete.ProductId);
            
            if (productFlowDeleted != null)
            {
                _unitOfWork.ProductFlow.Remove(productFlowDeleted);
            }
            
            int purchaseOrderId = purchaseDetailDelete.PurchaseOrderId;
            _unitOfWork.PurchaseDetail.Remove(purchaseDetailDelete);
            _unitOfWork.Save();

            return RedirectToAction("Upsert", "PurchaseOrder", new { id = purchaseOrderId });
        }
    }
}
