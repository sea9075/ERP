using ERP.Models.BasicInformation;
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

        [ValidateNever]
        [DisplayName("進貨單號")]
        public string PurchaseOrderNumber { get; set; }

        [Required]
        [DisplayName("廠商單號")]
        public string SupplierDeliverOrder {  get; set; }

        [DisplayName("進貨日期")]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayName("進貨總價")]
        public int TotalPrice { get; set; }

        public DateTime Timeset { get; set; }

        [Required]
        [ValidateNever]
        [DisplayName("進貨廠商")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        [ValidateNever]
        public Supplier Supplier { get; set; }
    }
}
