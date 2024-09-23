using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase.PurchaseVM;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class PurchaseDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        public IActionResult Create(PurchaseDetailVM purchaseDetailVM)
        {
            if (ModelState.IsValid)
            {
                purchaseDetailVM.PurchaseDetail.TotalPrice = purchaseDetailVM.PurchaseDetail.Cost * purchaseDetailVM.PurchaseDetail.Quantity;
                _unitOfWork.PurchaseDetail.Add(purchaseDetailVM.PurchaseDetail);
            }
            _unitOfWork.Save();
            return RedirectToAction("Upsert", "PurchaseOrder", new {id = purchaseDetailVM.PurchaseDetail.PurchaseOrderId});
        }
    }
}
