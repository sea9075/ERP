using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IStockRepository : IRepository<Stock>
    {
        void Update(Stock stock);
    }
}
