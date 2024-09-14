using ERP.DataAccess.Repository.IRepository;
using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.DataAccess.Repository.Purchase;

namespace ERP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category {  get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IStockRepository Stock { get; private set; }
        public IProductRepository Product { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public IPurchasingOrderRepository PurchasingOrder { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Supplier = new SupplierRepository(_db);
            Stock = new StockRepository(_db);
            Product = new ProductRepository(_db);
            Inventory = new InventoryRepository(_db);
            PurchasingOrder = new PurchasingOrderRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
