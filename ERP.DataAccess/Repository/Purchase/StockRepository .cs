using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.Purchase
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
