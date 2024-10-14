using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        void Update(Department department);
    }
}
