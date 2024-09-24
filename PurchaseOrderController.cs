using ERP.DataAccess.Repository.IRepository;
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
                SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.SupplierId.ToString()
                }),
                PurchaseOrder = new PurchaseOrder(),
                PurchaseDetailVM = new PurchaseDetailVM
                {
                    ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.ProductId.ToString()
                    }),
                    PurchaseDetail = new PurchaseDetail(),
                    PurchaseDetailList = new List<PurchaseDetail>()
                }
            };

            if (id == null || id == 0)
            {
                return View(purchaseOrderVM);
            }
            else
            {
                purchaseOrderVM.PurchaseDetailVM.PurchaseDetailList = _unitOfWork.PurchaseDetail.GetAll(includeProperties: "Product").Where(u => u.PurchaseOrderId == id).ToList();
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
                    // 取得今天日期
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    string barCodePrefix = $"PO{todayDate}";

                    // 查詢今天已經存在的進貨單數量，並取得當天最大的流水號
                    var existringPurchaseOrderToday = _unitOfWork.PurchaseOrder.GetAll().Where(u => u.OrderNumber.StartsWith(barCodePrefix)).OrderByDescending(u => u.OrderNumber).FirstOrDefault();

                    // 假設今天的第一張進貨單
                    int nextSerialNumber = 1;

                    if (existringPurchaseOrderToday != null)
                    {
                        string lastBarCode = existringPurchaseOrderToday.OrderNumber;
                        // 取得進貨單流水號部分
                        int lastSerialNumber = int.Parse(lastBarCode.Substring(10));
                        nextSerialNumber = lastSerialNumber + 1;
                    }

                    // 產生新的進貨單條碼
                    purchaseOrderVM.PurchaseOrder.OrderNumber = $"{barCodePrefix}{nextSerialNumber.ToString().PadLeft(5, '0')}";

                    _unitOfWork.PurchaseOrder.Add(purchaseOrderVM.PurchaseOrder);
                    TempData["success"] = "新增進貨單成功";
                }
                else
                {
                    _unitOfWork.PurchaseOrder.Update(purchaseOrderVM.PurchaseOrder);
                    TempData["success"] = "修改進貨單成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Upsert", new {id = purchaseOrderVM.PurchaseOrder.PurchaseOrderId});
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

        [HttpPost]
        public IActionResult CreatePurchaseDetail(PurchaseDetailVM purchaseDetailVM)
        {
            if (ModelState.IsValid)
            {
                // 計算小計
                purchaseDetailVM.PurchaseDetail.TotalPrice = purchaseDetailVM.PurchaseDetail.Cost * purchaseDetailVM.PurchaseDetail.Quantity;

                // 計算庫存
                var inventory = _unitOfWork.Inventory.Get(u => u.ProductId== purchaseDetailVM.PurchaseDetail.ProductId);
                inventory.Quantity += purchaseDetailVM.PurchaseDetail.Quantity;
                _unitOfWork.Inventory.Update(inventory);

                _unitOfWork.PurchaseDetail.Add(purchaseDetailVM.PurchaseDetail);
                _unitOfWork.Save();
                TempData["success"] = "新增明細成功";
                return RedirectToAction("Upsert", new { id = purchaseDetailVM.PurchaseDetail.PurchaseOrderId });
            }

            TempData["error"] = "新增明細失敗";
            return RedirectToAction("Upsert", new { id = purchaseDetailVM.PurchaseDetail.PurchaseOrderId });
        }

        [HttpPost]
        public IActionResult DeletePurchaseDetail(int? PurchaseDetailId, int PurchaseOrderId)
        {
            var purchaseDetailDeleted = _unitOfWork.PurchaseDetail.Get(u => u.PurchaseDetailId == PurchaseDetailId);

            if (purchaseDetailDeleted != null)
            {
                _unitOfWork.PurchaseDetail.Remove(purchaseDetailDeleted);
                _unitOfWork.Save();
                TempData["success"] = "刪除成功";

                return RedirectToAction("Upsert", new {id = PurchaseOrderId});
            }

            return RedirectToAction("Upsert", new {id = PurchaseOrderId});

        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<PurchaseOrder> purchaseOrderList = _unitOfWork.PurchaseOrder.GetAll(includeProperties: "Supplier").ToList();
            return Json(new { data = purchaseOrderList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var purchasingOrderDeleted = _unitOfWork.PurchaseOrder.Get(u => u.PurchaseOrderId == id);
            var purchaseDetails = _unitOfWork.PurchaseDetail.GetAll().Where(u => u.PurchaseOrderId == id).ToList();

            if (purchasingOrderDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.PurchaseDetail.RemoveRange(purchaseDetails);
            _unitOfWork.PurchaseOrder.Remove(purchasingOrderDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
