using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;
using ERP.Models.Purchase;


namespace ERP.DataAccess.Repository.BasicInformation
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
