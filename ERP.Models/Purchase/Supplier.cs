using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.Purchase
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [DisplayName("供應商名稱")]
        public string SupplierName { get; set; }

        [Required]
        [DisplayName("供應商統一編號")]
        public string SupplierTaxIDNumber { get; set; }

        [DisplayName("供應商負責人")]
        public string? SupplierPersonInCharge { get; set; }

        [Required]
        [DisplayName("供應商電話")]
        public string SupplierTel { get; set; }

        [DisplayName("供應商地址")]
        public string? SupplierAddress { get; set; }

        [DisplayName("供應商網站")]
        [Url]
        public string? SupplierWeb { get; set; }

        [Required]
        [DisplayName("供應商聯絡人")]
        public string SupplierContact { get; set; }

        [Required]
        [DisplayName("供應商聯絡人電話")]
        public string SupplierContactTel { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("供應商 Email")]
        public string SupplierEmail { get; set; }

        [DisplayName("供應商付款方式")]
        public int? SupplierPaymentMethod { get; set; }

        [DisplayName("註記")]
        public string? SupplierNote { get; set; }

        public DateTime SupplierTimeset { get; set; }
    }
}
