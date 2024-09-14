using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface IPurchasingOrderRepository : IRepository<PurchasingOrder>
    {
        void Update(PurchasingOrder purchasingOrder);
    }
}
