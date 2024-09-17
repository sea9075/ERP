using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.IRepository.Purchase
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        void Update(PurchaseOrder purchaseOrder);
    }
}
