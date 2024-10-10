using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.IRepository.IBasicInformation
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        void Update(Inventory inventory);
    }
}
