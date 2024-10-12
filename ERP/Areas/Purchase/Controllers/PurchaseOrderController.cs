using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("Purchase")]
    public class PurchaseOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            List<PurchaseOrder> purchaseOrderList = _unitOfWork.PurchaseOrder.GetAll(includeProperties: "Supplier").ToList();
            return View(purchaseOrderList);
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
                PurchaseDetailList = _unitOfWork.PurchaseDetail.GetAll().Where(u => u.PurchaseOrderId == id).ToList(),
                PurchaseDetailVM = new()
                {
                    ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.ProductId.ToString()
                    }),
                    PurchaseDetail = new PurchaseDetail()
                }
            };

            if (id == 0 || id == null)
            {
                return View(purchaseOrderVM);
            }
            else
            {
                // 加總小計
                int sumSubPrice = _unitOfWork.PurchaseDetail.GetAll().Where(u => u.PurchaseOrderId == id).Sum(u => u.SubTotal);
                ViewBag.SumSubPrice = sumSubPrice;

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
                    // --------------------
                    // 建立進貨單號
                    // 取得當日日期
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    string purchasePrefix = $"X{todayDate}";

                    // 查詢今天已經存在的進貨單號，並取得當天最大的流水號
                    var exstingPurchaseOrderToday = _unitOfWork.PurchaseOrder.GetAll().Where(u => u.PurchaseOrderNumber.StartsWith(purchasePrefix)).OrderBy(u => u.PurchaseOrderNumber).FirstOrDefault();

                    // 假設今天的第一筆進貨單
                    int nextSerialNumber = 1;

                    // 判斷今天有新增進貨單
                    if (exstingPurchaseOrderToday != null)
                    {
                        // 取得今天最後一筆進貨單
                        string lastPurchaseOrder = exstingPurchaseOrderToday.PurchaseOrderNumber;

                        // 取得流水號部分
                        int lastSerialNumber = int.Parse(lastPurchaseOrder.Substring(9));
                        nextSerialNumber = lastSerialNumber + 1;
                    }

                    // 產生新的進貨單號
                    purchaseOrderVM.PurchaseOrder.PurchaseOrderNumber = $"{purchasePrefix}{nextSerialNumber.ToString().PadLeft(5, '0')}";
                    // --------------------

                    _unitOfWork.PurchaseOrder.Add(purchaseOrderVM.PurchaseOrder);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    // 加總小計
                    int sumSubPrice = _unitOfWork.PurchaseDetail.GetAll().Where(u => u.PurchaseOrderId == purchaseOrderVM.PurchaseOrder.PurchaseOrderId).Sum(u => u.SubTotal);
                    ViewBag.SumSubPrice = sumSubPrice;

                    _unitOfWork.PurchaseOrder.Update(purchaseOrderVM.PurchaseOrder);
                    TempData["success"] = "修改成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Upsert", new {id = purchaseOrderVM.PurchaseOrder.PurchaseOrderId});
            }
            else
            {
                if (purchaseOrderVM.PurchaseOrder.PurchaseOrderId == 0)
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
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
            }

            // 刪除進貨單明細
            List<PurchaseDetail> purchaseDetailDeletedList = _unitOfWork.PurchaseDetail.GetAll().Where(u => u.PurchaseOrderId == id).ToList();

            if (purchaseDetailDeletedList != null)
            {
                foreach (var obj in purchaseDetailDeletedList)
                {
                    // 減少庫存
                    Inventory inventoryDeleted = _unitOfWork.Inventory.Get(u => u.ProductId == obj.ProductId);
                    inventoryDeleted.Quantity -= obj.Quantity;

                    // 刪除流向
                    ProductFlow productFlowDeleted = _unitOfWork.ProductFlow.Get(u => u.Timeset == obj.Timeset && u.ProductId == obj.ProductId);
                    _unitOfWork.ProductFlow.Remove(productFlowDeleted);
                }

                // 刪除訂單明細
                _unitOfWork.PurchaseDetail.RemoveRange(purchaseDetailDeletedList);
            }

            PurchaseOrder purchaseOrderDeleted = _unitOfWork.PurchaseOrder.Get(u => u.PurchaseOrderId == id);
            _unitOfWork.PurchaseOrder.Remove(purchaseOrderDeleted);
            TempData["success"] = "刪除成功";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
