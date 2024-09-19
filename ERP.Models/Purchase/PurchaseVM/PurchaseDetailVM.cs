using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.Purchase.PurchaseVM
{
    public class PurchaseDetailVM
    {
        public PurchaseDetail PurchaseDetail { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }

        public List<PurchaseDetail> PurchaseDetailList { get; set; }
    }
}
