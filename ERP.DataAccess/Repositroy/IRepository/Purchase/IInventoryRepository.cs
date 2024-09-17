using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.IRepository.Purchase
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        void Update(Inventory inventory);
    }
}
