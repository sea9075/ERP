using ERP.DataAccess.Repository.IRepository;
using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.DataAccess.Repository.Purchase;
using ERP.DataAccess.Repositroy.IRepository.Purchase;
using ERP.DataAccess.Repositroy.Purchase;

namespace ERP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category {  get; private set; }
        public IProductRepository Product { get; private set; }
        public IStockRepository Stock { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public ISupplierRepository Supplier { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Stock = new StockRepository(_db);
            Inventory = new InventorRepository(_db);
            Supplier = new SupplierRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}