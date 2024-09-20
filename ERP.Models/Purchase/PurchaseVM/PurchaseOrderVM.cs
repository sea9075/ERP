using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.Purchase.PurchaseVM
{
    public class PurchaseOrderVM
    {
        public PurchaseOrder PurchaseOrder { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}
