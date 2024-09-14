using ERP.DataAccess.Repository.IRepository.Purchase;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repository.Purchase
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        private ApplicationDbContext _db;
        public InventoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Inventory inventory)
        {
            _db.Inventory.Update(inventory);
        }
    }
}
