using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models.Purchase
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }

        [DisplayName("進貨單號")]
        public string? OrderNumber { get; set; }

        [Required(ErrorMessage = "請輸入廠商進貨單號")]
        [DisplayName("廠商進貨單號")]
        public string SupplierOrderNumber { get; set; }

        [Required(ErrorMessage = "請輸入進貨日期")]
        [DisplayName("進貨日期")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "請輸入進貨單總價")]
        [DisplayName("進貨單總價")]
        public int TotalPrice { get; set; }

        [DisplayName("備註")]
        public string? Note { get; set; }

        public DateTime Timeset { get; set; }

        [ValidateNever]
        [DisplayName("進貨廠商")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        [ValidateNever]
        public Supplier Supplier { get; set; }
    }
}
