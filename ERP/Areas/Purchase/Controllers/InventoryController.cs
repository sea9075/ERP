using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class InventoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Inventory> inventoryList = _unitOfWork.Inventory.GetAll(includeProperties: "Product,Stock").ToList();
            return View(inventoryList);
        }

        public IActionResult Upsert(int? id)
        {
            InventoryVM inventoryVM = new()
            {
                ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ProductName,
                    Value = u.ProductId.ToString()
                }),
                StockList = _unitOfWork.Stock.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StockName,
                    Value = u.StockId.ToString()
                }),
                Inventory = new Inventory()
            };

            if (id == null || id == 0)
            {
                return View(inventoryVM);
            }
            else
            {
                inventoryVM.Inventory = _unitOfWork.Inventory.Get(u => u.InventoryId == id);
                return View(inventoryVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(InventoryVM inventoryVM)
        {
            if (ModelState.IsValid)
            {
                if (inventoryVM.Inventory.InventoryId == 0)
                {
                    inventoryVM.Inventory.Quantity = 0;
                    _unitOfWork.Inventory.Add(inventoryVM.Inventory);
                    TempData["success"] = "新增商品庫存成功";
                }
                else
                {
                    _unitOfWork.Inventory.Update(inventoryVM.Inventory);
                    TempData["success"] = "修改商品庫存成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (inventoryVM.Inventory.InventoryId == 0)
                {
                    TempData["error"] = "新增商品庫存失敗";
                }
                else
                {
                    TempData["error"] = "修改商品庫存失敗";
                }
                return View(inventoryVM.Inventory);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<Inventory> inventoryList = _unitOfWork.Inventory.GetAll(includeProperties: "Product,Stock").ToList();
            return Json(new { data = inventoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var inventoryDeleted = _unitOfWork.Inventory.Get(u => u.InventoryId == id);

            if (inventoryDeleted == null)
            {
                return Json(new {success = false, message = "刪除失敗" });
            }

            _unitOfWork.Inventory.Remove(inventoryDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
