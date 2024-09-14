using ERP.DataAccess.Repository.IRepository.Purchase;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {  get; }
        ISupplierRepository Supplier { get; }
        IStockRepository Stock { get; }
        IProductRepository Product { get; }
        IInventoryRepository Inventory { get; }
        void Save();
    }
}
