using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.Purchase.PurchaseVM
{
    public class PurchasingOrderVM
    {
        public PurchasingOrder PurchasingOrder { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}
