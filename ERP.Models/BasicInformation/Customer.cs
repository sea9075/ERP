using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class Customer
    {
        [Key]
        public int CustomerId {  get; set; }

        [Required(ErrorMessage = "請輸入姓名")]
        [DisplayName("*顧客姓名")]
        public string Name { get; set; }

        [DisplayName("顧客性別")]
        public int? Gender { get; set; }

        [Required(ErrorMessage = "請輸入生日")]
        [DisplayName("*顧客生日")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "請輸入 Email")]
        [DisplayName("*顧客 Email")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string Email { get; set; }

        [DisplayName("電話")]
        [RegularExpression(@"^0\d{1,2}-?\d{6,8}$", ErrorMessage = "電話格式錯誤")]
        public string? Phone {  get; set; }

        [Required(ErrorMessage = "請輸入手機號碼")]
        [DisplayName("*顧客手機號碼")]
        [RegularExpression(@"^09\d{2}-?\d{3}-?\d{3}$", ErrorMessage = "手機格式錯誤")]
        public string CellPhone { get; set; }

        [DisplayName("地址")]
        public string? Address { get; set; }

        [DisplayName("描述")]
        public string? Description { get; set; }

        public DateTime Timeset { get; set; }
    }
}
