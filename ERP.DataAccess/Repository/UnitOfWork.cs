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
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Supplier = new SupplierRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
