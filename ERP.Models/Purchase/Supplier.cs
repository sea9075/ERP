using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ERP.Models.Purchase
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "請輸入廠商名稱")]
        [DisplayName("廠商名稱")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9]+$", ErrorMessage = "廠商名稱只能包含中文、英文和數字")]
        [MaxLength(30, ErrorMessage = "廠商名稱不能超過 30 字")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入廠商統一編號")]
        [DisplayName("統一編號")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "統一編號必須是 8 位數字")]
        public string TaxId { get; set; }

        [Required(ErrorMessage = "請輸入廠商電話")]
        [DisplayName("廠商電話")]
        [RegularExpression(@"^(0[2-8])[-]?\d{7,8}$", ErrorMessage = "廠商電話格式不正確，請輸入正確的市話號碼")]
        public string Telephone { get; set; }

        [DisplayName("廠商傳真")]
        [RegularExpression(@"^(0[2-8])[-]?\d{7,8}$", ErrorMessage = "廠商傳真格式不正確，請輸入正確的傳真號碼")]
        public string? Fax { get; set; }

        [DisplayName("廠商地址")]
        [MaxLength(100, ErrorMessage = "廠商地址不能超過 100 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s\-]+$", ErrorMessage = "廠商地址只能包含中文、英文、數字和 -")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "請輸入廠商聯絡人")]
        [DisplayName("廠商聯絡人")]
        [MaxLength(20, ErrorMessage = "廠商聯絡人名稱不能超過 20 字")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9]+$", ErrorMessage = "廠商聯絡人名稱只能包含中文、英文和數字")]
        public string ContactPerson { get; set; }

        [DisplayName("聯絡人電話")]
        [RegularExpression(@"^(0[2-8][-]?\d{7,8}|09\d{8})$", ErrorMessage = "電話格式不正確，請輸入正確的市話或手機號碼")]
        public string? ContactPhone { get; set; }

        [DisplayName("聯絡人 Email")]
        [EmailAddress(ErrorMessage = "請輸入正確的 Email 格式")]
        public string? ContactEmail { get; set; }

        [DisplayName("備註")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s]+$", ErrorMessage = "備註只能包含中文、英文、數字和空白，不能使用特殊符號")]
        public string? Description { get; set; }

        public DateTime Timeset { get; set; }
    }
}
