using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("分類名稱")]
        public string Name { get; set; }

        [Required]
        public DateTime Timeset { get; set; }
    }
}
