using ERP.Models.Purchase;
using MyBlog.DataAccess.Repository.IRepository;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        void Update(Supplier supplier);
    }
}
