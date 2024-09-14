using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
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
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // 取得 wwwroot 路徑
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    // 將檔案名稱變更為 GUID，並存入 wwwroot/images/product
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    // 如果有新圖片上傳，刪除舊圖片
                    if (!string.IsNullOrEmpty(productVM.Product.ProductImage))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ProductImage.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ProductImage = @"\images\product\" + fileName;
                }

                if (productVM.Product.ProductId == 0)
                {
                    // 取得今天日期
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    string barCodePrefix = $"P{todayDate}";

                    // 查詢今天已經存在的商品數量，並取得當天最大的流水號
                    var existingProductToday = _unitOfWork.Product.GetAll().Where(u => u.ProductBarCode.StartsWith(barCodePrefix)).OrderByDescending(u => u.ProductBarCode).FirstOrDefault();

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
                    productVM.Product.ProductBarCode = $"{barCodePrefix}{nextSerialNumber.ToString().PadLeft(5, '0')}";

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

            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productDeleted = _unitOfWork.Product.Get(u => u.ProductId == id);

            // 刪除圖片檔案
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productDeleted.ProductImage.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            if (productDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Product.Remove(productDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }

        #endregion
    }
}
