using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
