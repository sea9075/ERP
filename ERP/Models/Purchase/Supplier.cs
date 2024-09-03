using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.Purchase
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "請輸入廠商名稱")]
        [DisplayName("供應商名稱")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9]{1,15}$", ErrorMessage = "供應商名稱只能包含中文、英文、數字，且不能超過15字")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "請輸入供應商統一編號")]
        [DisplayName("供應商統一編號")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "統一編號必須是8位數字")]
        public string SupplierTaxIDNumber { get; set; }

        [Required(ErrorMessage = "請輸入供應商聯絡人")]
        [DisplayName("供應商聯絡人")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z]{1,10}$", ErrorMessage = "聯絡人只能包含中文或英文，且不能超過10個字")]
        public string SupplierContact {  get; set; }

        [Required(ErrorMessage = "請輸入供應商電話")]
        [DisplayName("供應商聯絡電話")]
        [RegularExpression(@"^0\d{1,2}-?\d{7,8}$", ErrorMessage = "電話必須是有效的市話號碼")]
        public string SupplierPhone { get; set; }

        [Required(ErrorMessage = "請輸入供應商傳真")]
        [DisplayName("供應商傳真")]
        [RegularExpression(@"^0\d{1,2}-?\d{7,8}$", ErrorMessage = "供應商傳真必須是有效的傳真號碼")]
        public string? SupplierFax { get; set; }

        [Required(ErrorMessage = "請輸入聯絡人手機號碼")]
        [DisplayName("聯絡人手機號碼")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "手機號碼必須是有效的手機號碼，以09開頭，後接8位數字")]
        public string? SupplierCellPhoneNumber { get; set; }

        [Required]
        [DisplayName("供應商 Email")]
        [EmailAddress(ErrorMessage = "請輸入正確的 Email 格式")]
        public string SupplierEmail { get; set; }

        [DisplayName("供應商地址")]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\-]{1,30}$", ErrorMessage = "供應商地址只能包含中文、英文、數字和連字符，且不能超過30個字")]
        public string? SupplierAddress { get; set; }

        [DisplayName("供應商網站")]
        [Url(ErrorMessage = "請輸入正確的網站格式")]
        public string? SupplierWeb {  get; set; }

        [DisplayName("供應商付款方式")]
        public int? SupplierPaymentMethod { get; set; }

        [Required]
        public DateTime SupplierTimeset { get; set; }
    }
}
