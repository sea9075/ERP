using ERP.DataAccess.Repository.IRepository.IBasicInformation;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMyCompanyRepository MyCompany {  get; }
        ICategoryRepository Category { get; }
        IStockRepository Stock { get; }
        IProductRepository Product { get; }
        ISupplierRepository Supplier { get; }
        IInventoryRepository Inventory { get; }
        IProductFlowRepository ProductFlow { get; }
        IPurchaseOrderRepository PurchaseOrder { get; }
        IPurchaseDetailRepository PurchaseDetail { get; }
        void Save();
    }
}
