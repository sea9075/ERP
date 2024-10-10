using ERP.DataAccess.Repository.IRepository.IBasicInformation;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMyCompanyRepository MyCompany {  get; }
        ICategoryRepository Category { get; }
        IStockRepository Stock { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
