using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Index()
        {
            List<PurchaseDetail> PurchaseDetailList = _unitOfWork.PurchaseDetail.GetAll(includeProperties: "Supplier").ToList();
            return View(PurchaseDetailList);
        }

        public IActionResult Upsert(int? id)
        {
            PurchaseDetailVM purchaseDetailVM = new()
            {
                ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ProductId.ToString()
                }),
                PurchaseDetail = new PurchaseDetail()
                
            };

            if (id == null || id == 0)
            {
                return View(purchaseDetailVM);
            }
            else
            {
                purchaseDetailVM.PurchaseDetail = _unitOfWork.PurchaseDetail.Get(u => u.PurchaseDetailId == id);
                return View(purchaseDetailVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PurchaseDetailVM purchaseDetailVM)
        {
            if (ModelState.IsValid)
            {
                if (purchaseDetailVM.PurchaseDetail.PurchaseDetailId == 0)
                {
                    _unitOfWork.PurchaseDetail.Add(purchaseDetailVM.PurchaseDetail);
                    TempData["success"] = "新增進貨單成功";
                }
                else
                {
                    _unitOfWork.PurchaseDetail.Update(purchaseDetailVM.PurchaseDetail);
                    TempData["success"] = "修改進貨單成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (purchaseDetailVM.PurchaseDetail.PurchaseDetailId == 0)
                {
                    TempData["error"] = "新增進貨單失敗";
                }
                else
                {
                    TempData["error"] = "修改進貨單失敗";
                }
                return View(purchaseDetailVM.PurchaseDetail);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<PurchaseDetail> inventoryList = _unitOfWork.PurchaseDetail.GetAll(includeProperties: "Supplier").ToList();
            return Json(new { data = inventoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var purchasingDetailDeleted = _unitOfWork.PurchaseDetail.Get(u => u.PurchaseDetailId == id);

            if (purchasingDetailDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.PurchaseDetail.Remove(purchasingDetailDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
