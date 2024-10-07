using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IMyCompanyRepository : IRepository<MyCompany>
    {
        void Update(MyCompany myCompany);
    }
}
