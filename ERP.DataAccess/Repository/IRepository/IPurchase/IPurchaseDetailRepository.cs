using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IPurchaseDetailRepository : IRepository<PurchaseDetail>
    {
        void Update(PurchaseDetail purchaseDetail);
    }
}
