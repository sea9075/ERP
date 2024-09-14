using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.Purchase
{
    public class PurchasingOrder
    {
        [Key]
        public int PurchasingOrderId { get; set; }

        [ValidateNever]
        [DisplayName("進貨單序號")]
        public string PurchasingOrderCode { get; set; }

        [DisplayName("進貨單廠商序號")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s\-]+$", ErrorMessage = "廠商序號只能包含中文、英文、數字和 -")]
        public string SupplierPurchaseId { get; set; }

        [Required(ErrorMessage = "請輸入進貨日期")]
        [DisplayName("進貨日期")]
        public DateTime PurchaseDate { get; set; }

        [DisplayName("進貨單備註")]
        public string? PurchaseNote { get; set; }

        [Required(ErrorMessage = "請輸入進貨單總金額")]
        [DisplayName("進貨總金額")]
        public int PurchaseTotalPrice { get; set; }

        public DateTime TimeSet { get; set; }

        [DisplayName("進貨廠商名稱")]
        [ValidateNever]
        public int SupplierId {  get; set; }

        [ValidateNever]
        public Supplier Supplier {  get; set; }
    }
}
