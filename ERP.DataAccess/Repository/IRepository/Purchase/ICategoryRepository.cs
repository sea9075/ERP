using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
