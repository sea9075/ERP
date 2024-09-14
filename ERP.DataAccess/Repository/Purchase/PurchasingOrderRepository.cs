using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.Purchase
{
    public class PurchasingOrderRepository : Repository<PurchasingOrder>, IPurchasingOrderRepository
    {
        private ApplicationDbContext _db;
        public PurchasingOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchasingOrder purchasingOrder)
        {
            _db.PurchasingOrders.Update(purchasingOrder);
        }
    }
}
