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

        public async Task<IActionResult> Index()
        {
            List<ProductFlow> productFlowList = (await _unitOfWork.ProductFlow.GetAllAsync(includeProperties: "Product")).ToList();
            return View(productFlowList);
        }
    }
}
