using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.BasicInformation;
using ERP.DataAccess.Repository.IRepository;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;

namespace ERP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IMyCompanyRepository MyCompany {  get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IStockRepository Stock { get; private set; }
        public IProductRepository Product { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public IProductFlowRepository ProductFlow { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            MyCompany = new MyCompanyRepository(_db);
            Category = new CategoryRepository(_db);
            Stock = new StockRepository(_db);
            Product = new ProductRepository(_db);
            Supplier = new SupplierRepository(_db);
            Inventory = new InventoryRepository(_db);
            ProductFlow = new ProductFlowRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
