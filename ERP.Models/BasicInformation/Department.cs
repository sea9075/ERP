using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "請輸入部門名稱")]
        [DisplayName("*部門名稱")]
        public string Name { get; set; }

        public DateTime Timeset { get; set; }
    }
}
