using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.Models.Purchase;
using MyBlog.DataAccess.Repository;

namespace ERP.DataAccess.Repository.Purchase
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private ApplicationDbContext _db;
        public SupplierRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Supplier supplier)
        {
            _db.Suppliers.Update(supplier);
        }
    }
}
