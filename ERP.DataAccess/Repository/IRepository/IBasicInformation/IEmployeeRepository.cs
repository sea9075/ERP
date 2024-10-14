using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void Update(Employee employee);
    }
}
