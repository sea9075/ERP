using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models.Purchase
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [ValidateNever]
        public string ProductBarCode { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱")]
        [DisplayName("商品名稱")]
        [MaxLength(100, ErrorMessage = "商品名稱不能超過 100 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s]+$", ErrorMessage = "商品名稱只能包含中文、英文、數字和空格，不能包含特殊符號")]
        public string ProductName { get; set; }

        [DisplayName("商品描述")]
        [MaxLength(255, ErrorMessage = "商品描述不能超過 255 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s]+$", ErrorMessage = "商品描述只能包含中文、英文、數字和空格，不能包含特殊符號")]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "請輸入商品成本")]
        [DisplayName("商品成本")]
        public int ProductCost { get; set; }

        [Required(ErrorMessage = "請輸入商品價格")]
        [DisplayName("商品建議售價")]
        public int ProductPrice { get; set; }

        [Required(ErrorMessage = "請輸入商品單位")]
        [DisplayName("商品單位")]
        public string ProductUnit {  get; set; }

        [DisplayName("商品折扣")]
        public int? ProductDiscount { get; set; }

        [DisplayName("商品圖片")]
        [ValidateNever]
        public string? ProductImage { get; set; }

        public DateTime TimeSet { get; set; }

        [ValidateNever]
        [DisplayName("商品類別")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        [DisplayName("供應商名稱")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        [ValidateNever]
        public Supplier Supplier { get; set; }
    }
}
