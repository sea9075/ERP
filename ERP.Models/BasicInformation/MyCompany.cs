using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class MyCompany
    {
        [Key]
        public int MyCompanyId { get; set; }

        [Required(ErrorMessage = "請輸入公司名稱")]
        [DisplayName("*公司名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入統一編號")]
        [DisplayName("*統一編號")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "統一編號必須為 8 位數字")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "統一編號格式錯誤")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "請輸入公司負責人")]
        [DisplayName("*公司負責人")]
        public string PersonInCharge { get; set; }

        [Required(ErrorMessage = "請輸入公司電話")]
        [DisplayName("*公司電話")]
        [RegularExpression(@"^(0\d{1,2}-?\d{6,8}|09\d{2}-?\d{3}-?\d{3})$", ErrorMessage = "電話格式錯誤")]
        public string Phone {  get; set; }

        [Required(ErrorMessage = "請輸入公司地址")]
        [DisplayName("*公司地址")]
        public string Address { get; set; }

        [DisplayName("公司 Email")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string? Email { get; set; }

        [DisplayName("公司網站")]
        public string? Url { get; set; }

        public DateTime Timeset { get; set; }
    }
}
