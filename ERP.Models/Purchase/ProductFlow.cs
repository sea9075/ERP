using ERP.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models.Purchase
{
    public class ProductFlow
    {
        [Key]
        public int ProductFlowId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Action {  get; set; }

        public int Quantity { get; set; }

        public DateTime Timeset { get; set; }

        [ValidateNever]
        public int ProductId { get; set; }

        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
