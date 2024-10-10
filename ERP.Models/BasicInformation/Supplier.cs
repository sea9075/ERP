using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [DisplayName("*廠商名稱")]
        public string Name { get; set; }

        [Required]
        [DisplayName("*廠商統一編號")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "統一編號必須為 8 位數字")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "統一編號格式錯誤")]
        public string TaxNumber { get; set; }

        [DisplayName("廠商地址")]
        public string? Address { get; set; }

        [Required]
        [DisplayName("*聯絡人")]
        public string ContactPerson { get; set; }

        [Required]
        [DisplayName("*連絡電話")]
        [RegularExpression(@"^(0\d{1,2}-?\d{6,8}|09\d{2}-?\d{3}-?\d{3})$", ErrorMessage = "電話格式錯誤")]
        public string Phone {  get; set; }

        [DisplayName("聯絡人手機")]
        [RegularExpression(@"^09\d{2}-?\d{3}-?\d{3}$", ErrorMessage = "手機格式錯誤")]
        public string? CellPhone { get; set; }

        [DisplayName("聯絡人 Email")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string? Email { get; set; }

        [DisplayName("描述")]
        public string? Description { get; set; }

        public DateTime Timeset { get; set; }
    }
}
