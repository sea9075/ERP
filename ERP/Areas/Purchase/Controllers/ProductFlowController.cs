using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class ProductFlowController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductFlowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? id)
        {
            List<ProductFlow> productFlowList = _unitOfWork.ProductFlow.GetAll(includeProperties: "Product").Where(u => u.ProductId == id).ToList();
            return View();
        }
    }
}
