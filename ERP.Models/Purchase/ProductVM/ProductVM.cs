using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.Purchase.ProductVM
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}
