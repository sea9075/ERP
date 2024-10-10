using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.BasicInformation.Controllers
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
                StockList = _unitOfWork.Stock.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.StockId.ToString()
                }),
                ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ProductId.ToString()
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
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Inventory.Update(inventoryVM.Inventory);
                    TempData["success"] = "修改成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (inventoryVM.Inventory.InventoryId == 0)
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

            Inventory inventoryDeleted = _unitOfWork.Inventory.Get(u => u.InventoryId == id);
            _unitOfWork.Inventory.Remove(inventoryDeleted);
            TempData["success"] = "刪除成功";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
