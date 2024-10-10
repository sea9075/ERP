using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }

        [Required(ErrorMessage = "請輸入倉庫名稱")]
        [DisplayName("*倉庫名稱")]
        public string Name { get; set; }

        [DisplayName("倉庫地址")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "請輸入倉庫負責人")]
        [DisplayName("*倉庫負責人")]
        public string PersonInCharge { get; set; }

        [DisplayName("倉庫電話")]
        [RegularExpression(@"^0\d{1,2}-?\d{6,8}$", ErrorMessage = "電話格式錯誤")]
        public string? Phone {  get; set; }

        [DisplayName("負責人手機")]
        [RegularExpression(@"^09\d{2}-?\d{3}-?\d{3}$", ErrorMessage = "手機格式錯誤")]
        public string? CellPhone { get; set; }

        [DisplayName("描述")]
        public string? Description { get; set; }

        public DateTime Timeset { get; set; }
    }
}
