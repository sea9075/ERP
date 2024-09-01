using ERP.DataAccess.Repository.IRepository;
using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.DataAccess.Repository.Purchase;

namespace ERP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ISupplierRepository Supplier {  get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Supplier = new SupplierRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
