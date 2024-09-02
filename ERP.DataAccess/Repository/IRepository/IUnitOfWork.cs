using ERP.DataAccess.Repository.IRepository.Purchase;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISupplierRepository Supplier {  get; }
        ICategoryRepository Category { get; }
        void Save();
    }
}
