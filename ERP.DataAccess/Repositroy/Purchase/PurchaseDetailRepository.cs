using ERP.DataAccess.Repository;
using ERP.DataAccess.Repositroy.IRepository.Purchase;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.Purchase
{
    public class PurchaseDetailRepository : Repository<PurchaseDetail>, IPurchaseDetailRepository
    {
        private ApplicationDbContext _db;
        public PurchaseDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchaseDetail purchaseDetail)
        {
            _db.PurchaseDetail.Update(purchaseDetail);
        }
    }
}
