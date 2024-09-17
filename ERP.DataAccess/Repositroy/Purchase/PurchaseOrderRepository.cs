using ERP.DataAccess.Repository;
using ERP.DataAccess.Repositroy.IRepository.Purchase;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.Purchase
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        private ApplicationDbContext _db;
        public PurchaseOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchaseOrder purchaseOrder)
        {
            _db.PurchaseOrders.Update(purchaseOrder);
        }
    }
}
