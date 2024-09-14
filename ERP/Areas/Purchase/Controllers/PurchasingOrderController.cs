using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class PurchasingOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchasingOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<PurchasingOrder> inventoryList = _unitOfWork.PurchasingOrder.GetAll(includeProperties: "Supplier").ToList();
            return View(inventoryList);
        }

        public IActionResult Upsert(int? id)
        {
            PurchasingOrderVM purchasingOrderVM = new()
            {
                SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierId.ToString()
                }),
                PurchasingOrder = new PurchasingOrder()
            };

            if (id == null || id == 0)
            {
                return View(purchasingOrderVM);
            }
            else
            {
                purchasingOrderVM.PurchasingOrder = _unitOfWork.PurchasingOrder.Get(u => u.PurchasingOrderId == id);
                return View(purchasingOrderVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PurchasingOrderVM purchasingOrderVM)
        {
            if (ModelState.IsValid)
            {
                if (purchasingOrderVM.PurchasingOrder.PurchasingOrderId == 0)
                {
                    // 取得今天日期
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    string barCodePrefix = $"PO{todayDate}";

                    // 查詢今天已經存在的商品數量，並取得當天最大的流水號
                    var existringPurchasingOrderToday = _unitOfWork.PurchasingOrder.GetAll().Where(u => u.PurchasingOrderCode.StartsWith(barCodePrefix)).OrderByDescending(u => u.PurchasingOrderCode).FirstOrDefault();

                    // 假設今天的第一件商品
                    int nextSerialNumber = 1;
                    
                    if(existringPurchasingOrderToday != null)
                    {
                        string lastBarCode = existringPurchasingOrderToday.PurchasingOrderCode;
                        // 取得商品流水號部分
                        int lastSerialNumber = int.Parse(lastBarCode.Substring(10));
                        nextSerialNumber = lastSerialNumber + 1;
                    }

                    // 產生新的商品條碼
                    purchasingOrderVM.PurchasingOrder.PurchasingOrderCode = $"{barCodePrefix}{nextSerialNumber.ToString().PadLeft(5, '0')}";
                    
                    _unitOfWork.PurchasingOrder.Add(purchasingOrderVM.PurchasingOrder);
                    TempData["success"] = "新增商品庫存成功";
                }
                else
                {
                    _unitOfWork.PurchasingOrder.Update(purchasingOrderVM.PurchasingOrder);
                    TempData["success"] = "修改商品庫存成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (purchasingOrderVM.PurchasingOrder.PurchasingOrderId == 0)
                {
                    TempData["error"] = "新增商品庫存失敗";
                }
                else
                {
                    TempData["error"] = "修改商品庫存失敗";
                }
                return View(purchasingOrderVM.PurchasingOrder);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<PurchasingOrder> inventoryList = _unitOfWork.PurchasingOrder.GetAll(includeProperties: "Supplier").ToList();
            return Json(new { data = inventoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var purchasingOrderDeleted = _unitOfWork.PurchasingOrder.Get(u => u.PurchasingOrderId == id);

            if (purchasingOrderDeleted == null)
            {
                return Json(new {success = false, message = "刪除失敗" });
            }

            _unitOfWork.PurchasingOrder.Remove(purchasingOrderDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
