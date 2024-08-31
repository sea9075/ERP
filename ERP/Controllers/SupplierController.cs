using ERP.Data;
using ERP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SupplierController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Supplier supplier = _db.Suppliers.Find(id);

            if (supplier == null)
            {
                return View(new Supplier());
            }
            else
            {
                return View(supplier);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.SupplierId == 0)
                {
                    _db.Suppliers.Add(supplier);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _db.Suppliers.Update(supplier);
                    TempData["success"] = "修改成功";
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                if (supplier.SupplierId == 0)
                {
                    TempData["error"] = "新增失敗";
                }
                else
                {
                    TempData["error"] = "修改失敗";
                }

                return View(supplier);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Supplier? supplier = _db.Suppliers.Find(id);
            if (supplier != null)
            {
                _db.Suppliers.Remove(supplier);
                TempData["success"] = "刪除成功";
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "刪除失敗";
                return RedirectToAction("Index");
            }
        }
    }
}
