using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.Purchase
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "請輸入類別名稱")]
        [DisplayName("商品類別名稱")]
        [StringLength(30, ErrorMessage = "類別名稱不能超過 30 個字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9]+$", ErrorMessage = "類別名稱只能包含中文、英文和數字")]
        public string CategoryName { get; set; }

        [Required]
        public DateTime TimeSet { get; set; }
    }
}
