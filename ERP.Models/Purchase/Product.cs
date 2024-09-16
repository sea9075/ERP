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

        [Required(ErrorMessage = "請輸入商品名稱")]
        [DisplayName("商品名稱")]
        [MaxLength(100, ErrorMessage = "商品名稱不能超過 100 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s]+$", ErrorMessage = "商品名稱只能包含中文、英文、數字和空格，不能包含特殊符號")]
        public string Name { get; set; }

        [ValidateNever]
        [DisplayName("商品條碼")]
        public string BarCode { get; set; }

        [DisplayName("商品描述")]
        [MaxLength(255, ErrorMessage = "商品描述不能超過 255 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s]+$", ErrorMessage = "商品描述只能包含中文、英文、數字和空格，不能包含特殊符號")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "請輸入商品價格")]
        [DisplayName("商品建議售價")]
        public int Price { get; set; }

        [ValidateNever]
        [DisplayName("商品圖片")]
        public string? Image {  get; set; }

        public DateTime Timeset { get; set; }

        [ValidateNever]
        [DisplayName("商品類別")]
        public int CategoryId { get; set; }

        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
