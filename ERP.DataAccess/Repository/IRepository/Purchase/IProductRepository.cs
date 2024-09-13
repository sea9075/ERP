using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
