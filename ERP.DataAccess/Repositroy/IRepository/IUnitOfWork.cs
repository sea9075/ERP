using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.DataAccess.Repositroy.IRepository.Purchase;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {  get; }
        IProductRepository Product { get; }
        IStockRepository Stock { get; }
        IInventoryRepository Inventory { get; }
        ISupplierRepository Supplier { get; }
        IPurchaseOrderRepository PurchaseOrder { get; }
        IPurchaseDetailRepository PurchaseDetail { get; }
        void Save();
    }
}