using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        void Update(PurchaseOrder purchaseOrder);
    }
}
