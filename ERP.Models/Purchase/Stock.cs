using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.Purchase
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }

        [Required(ErrorMessage = "請輸入倉庫名稱")]
        [DisplayName("倉庫名稱")]
        [MaxLength(20, ErrorMessage = "廠商名稱不能超過 20 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9]+$", ErrorMessage = "廠商名稱只能包含中文、英文和數字")]
        public string StockName { get; set; }

        [Required(ErrorMessage = "請輸入倉庫電話")]
        [DisplayName("倉庫電話")]
        [RegularExpression(@"^(0[2-8])[-]?\d{7,8}$", ErrorMessage = "廠商電話格式不正確，請輸入正確的市話號碼")]
        public string StockTel {  get; set; }

        [DisplayName("倉庫地址")]
        [MaxLength(100, ErrorMessage = "倉庫地址不能超過 100 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s\-]+$", ErrorMessage = "倉庫地址只能包含中文、英文、數字和 -")]
        public string? StockAddress { get; set; }

        [Required(ErrorMessage = "請輸入倉庫負責人")]
        [DisplayName("倉庫負責人")]
        [MaxLength(20, ErrorMessage = "倉庫負責人名稱不能超過 20 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9]+$", ErrorMessage = "倉庫負責人名稱只能包含中文、英文和數字")]
        public string StockPersonInChange { get; set; }

        [DisplayName("負責人手機")]
        [RegularExpression(@"^09\d{2}-?\d{3}-?\d{3}$", ErrorMessage = "請輸入正確的台灣手機號碼")]
        public string? StockPersonInChangeCellPhone { get; set; }

        public DateTime TimeSet { get; set; }
    }
}
