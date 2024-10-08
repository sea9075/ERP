using ERP.DataAccess.Repository.IRepository;
using ERP.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.BasicInformation.Controllers
{
    [Area("BasicInformation")]
    public class MyCompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MyCompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Upsert()
        {
            MyCompany myCompany = _unitOfWork.MyCompany.Get(u => u.MyCompanyId == 1);

            if (myCompany != null)
            {
                return View(myCompany);
            }
            else
            {
                myCompany = new MyCompany();
                return View(myCompany);
            }
        }

        [HttpPost]
        public IActionResult Upsert(MyCompany myCompany)
        {
            if (ModelState.IsValid)
            {
                if (myCompany.MyCompanyId == 0)
                {
                    _unitOfWork.MyCompany.Add(myCompany);
                    TempData["success"] = "新增成功";
                }
                else
                {
                    _unitOfWork.MyCompany.Update(myCompany);
                    TempData["success"] = "修改成功";
                }

                _unitOfWork.Save();
                return View(myCompany);
            }
            else
            {
                if (myCompany.MyCompanyId == 0)
                {
                    TempData["error"] = "新增失敗";
                }
                else
                {
                    TempData["error"] = "修改失敗";
                }

                return View(myCompany);
            }
        }
    }
}
