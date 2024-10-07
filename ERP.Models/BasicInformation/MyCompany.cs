using System.ComponentModel.DataAnnotations;

namespace ERP.Models.BasicInformation
{
    public class MyCompany
    {
        [Key]
        public int MyCompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TaxNumber { get; set; }

        [Required]
        public string PersonInCharge { get; set; }

        [Required]
        public string Phone {  get; set; }

        [Required]
        public string Address { get; set; }

        public string? Email { get; set; }

        public string? Url { get; set; }
    }
}
