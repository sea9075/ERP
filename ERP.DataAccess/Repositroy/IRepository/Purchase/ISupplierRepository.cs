using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.IRepository.Purchase
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        void Update(Supplier supplier);
    }
}
