using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }

        [Required]
        [DisplayName("*倉庫名稱")]
        public string Name { get; set; }

        [DisplayName("倉庫地址")]
        public string? Address { get; set; }

        [Required]
        [DisplayName("*倉庫負責人")]
        public string PersonInCharge { get; set; }

        [DisplayName("倉庫電話")]
        [RegularExpression(@"^(0[2-8]\d{1,2}-?\d{6,7})$", ErrorMessage = "電話格式錯誤")]
        public string? Phone {  get; set; }

        [DisplayName("負責人手機")]
        [RegularExpression(@"^(09\d{2}-?\d{3}-?\d{3})$", ErrorMessage = "手機格式錯誤")]
        public string? CellPhone { get; set; }

        [DisplayName("描述")]
        public string? Description { get; set; }

        [Required]
        public DateTime Timeset { get; set; }
    }
}
