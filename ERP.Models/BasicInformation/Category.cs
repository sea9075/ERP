using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "請輸入分類名稱")]
        [DisplayName("*分類名稱")]
        public string Name { get; set; }

        public DateTime Timeset { get; set; }
    }
}
