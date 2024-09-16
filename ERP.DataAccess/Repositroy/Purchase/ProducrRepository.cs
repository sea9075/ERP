using ERP.DataAccess.Repository;
using ERP.DataAccess.Repositroy.IRepository.Purchase;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.Purchase
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
