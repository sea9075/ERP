using ERP.Models.BasicInformation;
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

        [DisplayName("倉庫位置")]
        public string? StorageLocation { get; set; }

        [ValidateNever]
        [DisplayName("數量")]
        public int Quantity { get; set; }

        [ValidateNever]
        [DisplayName("倉庫名稱")]
        public int StockId { get; set; }

        [ValidateNever]
        [ForeignKey("StockId")]
        public Stock Stock { get; set; }

        [ValidateNever]
        [DisplayName("商品名稱")]
        public int ProductId { get; set; }

        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public DateTime Timeset { get; set; }
    }
}
