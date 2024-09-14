using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.Purchase
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        void Update(Inventory inventory);
    }
}
