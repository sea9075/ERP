using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models.BasicInformation
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱")]
        [DisplayName("*商品名稱")]
        public string Name { get; set; }

        [ValidateNever]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "請輸入商品售價")]
        [DisplayName("*商品價格")]
        public int Price { get; set; }

        [DisplayName("商品圖片")]
        public string? Image {  get; set; }

        [DisplayName("商品描述")]
        public string? Description { get; set; }

        public DateTime Timeset { get; set; }

        [Required(ErrorMessage = "請輸入商品分類")]
        [DisplayName("*商品分類")]
        [ValidateNever]
        public int CategoryId { get; set; }

        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
