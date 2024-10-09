using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
