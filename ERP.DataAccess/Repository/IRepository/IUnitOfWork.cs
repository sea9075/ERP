using ERP.DataAccess.Repository.IRepository.Purchase;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {  get; }
        ISupplierRepository Supplier { get; }
        void Save();
    }
}
