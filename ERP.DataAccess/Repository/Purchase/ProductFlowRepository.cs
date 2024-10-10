using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;
using ERP.Models.Purchase;


namespace ERP.DataAccess.Repository.BasicInformation
{
    public class ProductFlowRepository : Repository<ProductFlow>, IProductFlowRepository
    {
        private ApplicationDbContext _db;
        public ProductFlowRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductFlow productFlow)
        {
            _db.ProductFlows.Update(productFlow);
        }
    }
}
