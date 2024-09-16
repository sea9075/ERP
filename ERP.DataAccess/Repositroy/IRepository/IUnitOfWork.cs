using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.DataAccess.Repositroy.IRepository.Purchase;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {  get; }
        IProductRepository Product { get; }
        void Save();
    }
}