using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.Purchase
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("分類名稱")]
        public string CategoryName { get; set; }

        [DisplayName("分類描述")]
        public string? CategoryDescription { get; set; }

        [Required]
        public DateTime CategoryTimeset { get; set; }
    }
}
