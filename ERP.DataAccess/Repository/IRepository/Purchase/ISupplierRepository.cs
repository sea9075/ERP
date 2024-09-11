using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        void Update(Supplier supplier);
    }
}
