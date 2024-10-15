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

        public async Task<IActionResult> Index()
        {
            List<Inventory> inventoryList = (await _unitOfWork.Inventory.GetAllAsync(includeProperties: "Product,Stock")).ToList();
            return View(inventoryList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            InventoryVM inventoryVM = new()
            {
                StockList = (await _unitOfWork.Stock.GetAllAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.StockId.ToString()
                }),
                ProductList = (await _unitOfWork.Product.GetAllAsync()).Select(u => new SelectListItem
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
                inventoryVM.Inventory = await _unitOfWork.Inventory.GetAsync(u => u.InventoryId == id);
                return View(inventoryVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(InventoryVM inventoryVM)
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

                await _unitOfWork.SaveAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
            }

            Inventory inventoryDeleted = await _unitOfWork.Inventory.GetAsync(u => u.InventoryId == id);
            _unitOfWork.Inventory.Remove(inventoryDeleted);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "刪除成功";
            return RedirectToAction("Index");
        }
    }
}
