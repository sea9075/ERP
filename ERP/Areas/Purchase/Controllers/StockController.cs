using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class StockController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Stock> stockList = _unitOfWork.Stock.GetAll().ToList();
            return View(stockList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                Stock stock = new Stock();
                return View(stock);
            }
            else
            {
                Stock stock = _unitOfWork.Stock.Get(u => u.StockId == id);
                return View(stock);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Stock stock)
        {
            if (ModelState.IsValid)
            {
                if (stock.StockId == 0)
                {
                    _unitOfWork.Stock.Add(stock);
                    TempData["success"] = "新增類別成功";
                }
                else
                {
                    _unitOfWork.Stock.Update(stock);
                    TempData["success"] = "修改類別成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (stock.StockId == 0)
                {
                    TempData["error"] = "新增類別失敗";
                }
                else
                {
                    TempData["error"] = "修改類別失敗";
                }
                return View(stock);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<Stock> stockList = _unitOfWork.Stock.GetAll().ToList();
            return Json(new { data = stockList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var stockDeleted = _unitOfWork.Stock.Get(u => u.StockId == id);

            if (stockDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Stock.Remove(stockDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
