using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Update(Customer stock);
    }
}
