using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.BasicInformation;
using ERP.DataAccess.Repository.IRepository;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;

namespace ERP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IMyCompanyRepository MyCompany {  get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            MyCompany = new MyCompanyRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
