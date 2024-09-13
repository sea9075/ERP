using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll().ToList();
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                }),
                SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierId.ToString()
                }),
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Product.ProductId == 0)
                {
                    // 取得今天日期
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    string barCCodePrefix = $"P{todayDate}";

                    // 查詢今天已經存在的商品數量，並取得當天最大的流水號
                    var existingProductToday = _unitOfWork.Product.GetAll().Where(u => u.ProductBarCode.StartsWith(barCCodePrefix)).OrderByDescending(u => u.ProductBarCode).FirstOrDefault();

                    // 假設今天的第一件商品
                    int nextSerialNumber = 1;
                    if (existingProductToday != null)
                    {
                        string lastBarCode = existingProductToday.ProductBarCode;
                        // 取得商品流水號部分
                        int lastSerialNumber = int.Parse(lastBarCode.Substring(9));
                        nextSerialNumber = lastSerialNumber + 1;
                    }

                    // 產生新的商品條碼
                    productVM.Product.ProductBarCode = $"{barCCodePrefix}{nextSerialNumber.ToString().PadLeft(5, '0')}";

                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "新增商品成功";
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "修改商品成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (productVM.Product.ProductId == 0)
                {
                    TempData["error"] = "新增商品失敗";
                }
                else
                {
                    TempData["error"] = "修改商品失敗";
                }
                return View(productVM.Product);
            }
        }

        #region CALLS API
        [HttpGet]
        public IActionResult GetAll()
        {

            List<Product> productList = _unitOfWork.Product.GetAll().ToList();
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productDelete = _unitOfWork.Product.Get(u => u.ProductId == id);

            if (productDelete == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Product.Remove(productDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
