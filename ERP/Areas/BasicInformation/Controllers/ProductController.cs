using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using ERP.Models.BasicInformation.BasicInformationVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> productyList = (await _unitOfWork.Product.GetAllAsync(includeProperties: "Category")).ToList();
            return View(productyList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = (await _unitOfWork.Category.GetAllAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = await _unitOfWork.Product.GetAsync(u => u.ProductId ==  id);
                return View(productVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // 新增圖片
                // 取得 wwwroot 路徑
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // 判斷是否有傳入圖片
                if (file != null)
                {
                    // 將圖片名稱使用 GUID 代替
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // 取得放置圖片位置
                    string productImagePath = Path.Combine(wwwRootPath, @"images\product");

                    // 判斷是否存在舊圖片
                    if (!string.IsNullOrEmpty(productVM.Product.Image))
                    {
                        // 取得舊圖片的檔案位置
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.Image.TrimStart('\\'));

                        // 如果舊圖片位置有存在檔案
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            // 刪除舊圖片
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // 將上傳的圖片存入指定位置
                    using (var fileStream = new FileStream(Path.Combine(productImagePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // 將檔案位置存入資料表
                    productVM.Product.Image = @"\images\product\" + fileName;
                }

                if (productVM.Product.ProductId == 0)
                {
                    // 產生商品條碼
                    // 商品條碼格式為 P{今天日期}{流水號(00000)}
                    // 取得當日日期
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    string barcodePrefix = $"P{todayDate}";

                    // 查詢今天已經存在的商品數量，並取得當天最大的流水號
                    var existingProductToday = (await _unitOfWork.Product.GetAllAsync()).Where(u => u.Barcode.StartsWith(barcodePrefix)).OrderBy(u => u.Barcode).FirstOrDefault();

                    // 假設今天的第一件商品
                    int nextSerialNumber = 1;

                    // 判斷今天有新增商品
                    if (existingProductToday != null)
                    {
                        // 取得今天最後一筆新增商品
                        string lastBarcode = existingProductToday.Barcode;

                        // 取得流水號部分
                        int lastSerialNumber = int.Parse(lastBarcode.Substring(9));
                        nextSerialNumber = lastSerialNumber + 1;
                    }

                    // 產生新的商品條碼
                    productVM.Product.Barcode = $"{barcodePrefix}{nextSerialNumber.ToString().PadLeft(5, '0')}";

                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "修改成功";
                }

                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {
                if (productVM.Product.ProductId == 0)
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

            Product productDeleted = await _unitOfWork.Product.GetAsync(u => u.ProductId == id);

            // 刪除圖片
            // 取得舊圖片位置
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productDeleted.Image.TrimStart('\\'));

            // 如果舊圖片位置有存在檔案
            if (System.IO.File.Exists(oldImagePath))
            {
                // 刪除舊圖片
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productDeleted);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "刪除成功";
            return RedirectToAction("Index");
        }
    }
}
