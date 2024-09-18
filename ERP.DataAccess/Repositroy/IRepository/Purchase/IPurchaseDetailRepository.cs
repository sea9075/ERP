using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.IRepository.Purchase
{
    public interface IPurchaseDetailRepository : IRepository<PurchaseDetail>
    {
        void Update(PurchaseDetail purchaseDetail);
    }
}
