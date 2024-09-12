using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface IStockRepository : IRepository<Stock>
    {
        void Update(Stock stock);
    }
}
