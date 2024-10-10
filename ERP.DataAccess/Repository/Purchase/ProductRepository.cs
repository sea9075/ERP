using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;
using ERP.Models.Purchase;


namespace ERP.DataAccess.Repository.BasicInformation
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
            _db.Inventories.Update(inventory);
        }
    }
}
