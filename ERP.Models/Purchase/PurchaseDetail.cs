using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models.Purchase
{
    public class PurchaseDetail
    {
        [Key]
        public int PurchaseDetailId { get; set; }

        [DisplayName("數量")]
        public int Quantity { get; set; }

        [DisplayName("商品成本")]
        public int Cost { get; set; }

        [DisplayName("小計")]
        public int TotalPrice { get; set; }

        [ValidateNever]
        [DisplayName("商品名稱")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        [ValidateNever]
        [DisplayName("進貨單號")]
        public int PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")]
        [ValidateNever]
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
