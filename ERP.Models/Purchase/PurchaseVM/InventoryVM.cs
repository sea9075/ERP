using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.Purchase.PurchaseVM
{
    public class InventoryVM
    {
        public Inventory Inventory { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> StockList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
