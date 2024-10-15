using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
    public class StockController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<Stock> stockList = (await _unitOfWork.Stock.GetAllAsync()).ToList();
            return View(stockList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Stock stock = await _unitOfWork.Stock.GetAsync(u => u.StockId == id);

            if (stock != null)
            {
                return View(stock);
            }
            else
            {
                stock = new Stock();
                return View(stock);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Stock stock)
        {
            if (ModelState.IsValid)
            {
                if (stock.StockId == 0)
                {
                    _unitOfWork.Stock.Add(stock);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Stock.Update(stock);
                    TempData["success"] = "修改成功";
                }

                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {
                if (stock.StockId == 0)
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

            Stock stockDeleted = await _unitOfWork.Stock.GetAsync(u => u.StockId == id);
            _unitOfWork.Stock.Remove(stockDeleted);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "刪除成功";
            return RedirectToAction("Index");
        }
    }
}
