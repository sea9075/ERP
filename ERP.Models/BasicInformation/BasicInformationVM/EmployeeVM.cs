using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.BasicInformation.BasicInformationVM
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
}
