using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;
using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.BasicInformation
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private ApplicationDbContext _db;
        public StockRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Stock stock)
        {
            _db.Stocks.Update(stock);
        }
    }
}
