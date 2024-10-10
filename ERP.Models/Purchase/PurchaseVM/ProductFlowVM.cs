using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.Purchase.PurchaseVM
{
    public class ProductFlowVM
    {
        public ProductFlow ProductFlow { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
