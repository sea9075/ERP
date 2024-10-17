using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models.BasicInformation
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "請輸入員工姓名")]
        [DisplayName("*姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入身份證字號")]
        [DisplayName("*身份證字號")]
        public string NationalIdentificationNumber { get; set; }

        [DisplayName("性別")]
        public int? Gender {  get; set; }

        [Required(ErrorMessage = "請輸入員工生日")]
        [DisplayName("*出生年月日")]
        public DateTime BirthDate { get; set; }

        [DisplayName("電話")]
        [RegularExpression(@"^(0\d{1,2}-?\d{6,8}|09\d{2}-?\d{3}-?\d{3})$", ErrorMessage = "電話格式錯誤")]
        public string? Phone {  get; set; }

        [Required(ErrorMessage = "請輸入手機號碼")]
        [DisplayName("*手機號碼")]
        [RegularExpression(@"^09\d{2}-?\d{3}-?\d{3}$", ErrorMessage = "手機格式錯誤")]
        public string CellPhone { get; set; }

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string? Email { get; set; }

        [DisplayName("地址")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "請輸入緊急聯絡人姓名")]
        [DisplayName("*緊急聯絡人")]
        public string EmergencyContact { get; set; }

        [DisplayName("緊急聯絡人電話")]
        [RegularExpression(@"^(0\d{1,2}-?\d{6,8}|09\d{2}-?\d{3}-?\d{3})$", ErrorMessage = "電話格式錯誤")]
        public string? EmergencyContactPhone { get; set; }

        [Required(ErrorMessage = "請輸入緊急連絡人手機號碼")]
        [DisplayName("*緊急聯絡人手機號碼")]
        [RegularExpression(@"^09\d{2}-?\d{3}-?\d{3}$", ErrorMessage = "手機格式錯誤")]
        public string EmergencyContactCellPhone { get; set; }

        [Required(ErrorMessage = "請輸入登入帳號")]
        [DisplayName("*登入帳號")]
        public string Account {  get; set; }

        [DisplayName("*登入密碼")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密碼長度必須在 6 到 100 之間")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "密碼必須至少包含一個大寫字母和一個數字")]
        public string? Password { get; set; }

        [NotMapped] // 不映射到資料庫
        [Compare("Password" ,ErrorMessage = "密碼與確認密碼不一致")]
        [DisplayName("確認密碼")]
        [ValidateNever]
        public string? ConfirmPassword { get; set; }

        [DisplayName("員工照片")]
        public string? Image { get; set; }

        public DateTime Timeset { get; set; }

        [Required]
        [ValidateNever]
        [DisplayName("公司部門")]
        public int DepartmentId { get; set; }

        [ValidateNever]
        public Department Department { get; set; }
    }
}
