using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.IRepository.Purchase
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
