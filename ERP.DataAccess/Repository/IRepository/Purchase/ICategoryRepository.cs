using ERP.Models.Purchase;
using MyBlog.DataAccess.Repository.IRepository;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
