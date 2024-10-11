using ERP.Models.BasicInformation;
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

        [Required]
        [DisplayName("進貨成本")]
        public int Cost { get; set; }

        [Required]
        [DisplayName("進貨數量")]
        public int Quantity { get; set; }

        [DisplayName("小計")]
        public int SubTotal { get; set; }

        public DateTime Timeset { get; set; }

        [ValidateNever]
        public int PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")]
        [ValidateNever]
        public PurchaseOrder PurchaseOrder { get; set; }

        [ValidateNever]
        [DisplayName("商品名稱")]
        public int ProductId {  get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
