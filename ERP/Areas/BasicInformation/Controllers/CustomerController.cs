using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Customer> customerList = _unitOfWork.Customer.GetAll().ToList();
            return View(customerList);
        }

        public IActionResult Upsert(int? id)
        {
            Customer customer = _unitOfWork.Customer.Get(u => u.CustomerId == id);

            if (customer != null)
            {
                return View(customer);
            }
            else
            {
                customer = new Customer();
                return View(customer);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerId == 0)
                {
                    _unitOfWork.Customer.Add(customer);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.Customer.Update(customer);
                    TempData["success"] = "修改成功";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                if (customer.CustomerId == 0)
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

            Customer customerDeleted = _unitOfWork.Customer.Get(u => u.CustomerId == id);
            _unitOfWork.Customer.Remove(customerDeleted);
            TempData["success"] = "刪除成功";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
