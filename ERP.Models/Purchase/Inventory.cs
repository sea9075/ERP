using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models.Purchase
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "請輸入倉庫架位")]
        [DisplayName("倉庫架位")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s\-]+$", ErrorMessage = "倉庫架位只能包含中文、英文、數字和 -")]
        public string StorageBin { get; set; }

        [DisplayName("商品庫存數量")]
        public int? Quantity { get; set; }

        public DateTime Timeset { get; set; }

        [ValidateNever]
        [DisplayName("商品名稱")]
        public int ProductId { get; set; }

        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ValidateNever]
        [DisplayName("倉庫名稱")]
        public int StockId { get; set; }

        [ValidateNever]
        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
    }
}
