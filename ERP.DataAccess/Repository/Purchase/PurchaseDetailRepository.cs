using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;
using ERP.Models.Purchase;


namespace ERP.DataAccess.Repository.BasicInformation
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
