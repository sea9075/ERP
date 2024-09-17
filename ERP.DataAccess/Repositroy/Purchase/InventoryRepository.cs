using ERP.DataAccess.Repository;
using ERP.DataAccess.Repositroy.IRepository.Purchase;
using ERP.Models.Purchase;

namespace ERP.DataAccess.Repositroy.Purchase
{
    public class InventorRepository : Repository<Inventory>, IInventoryRepository
    {
        private ApplicationDbContext _db;
        public InventorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Inventory inventory)
        {
            _db.Inventory.Update(inventory);
        }
    }
}
