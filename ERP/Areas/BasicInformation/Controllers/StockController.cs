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

        public IActionResult Index()
        {
            List<Stock> stockList = _unitOfWork.Stock.GetAll().ToList();
            return View(stockList);
        }

        public IActionResult Upsert(int? id)
        {
            Stock stock = _unitOfWork.Stock.Get(u => u.StockId == id);

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
        public IActionResult Upsert(Stock stock)
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

                _unitOfWork.Save();
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
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
            }

            Stock stockDeleted = _unitOfWork.Stock.Get(u => u.StockId == id);
            _unitOfWork.Stock.Remove(stockDeleted);
            TempData["success"] = "刪除成功";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
