using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository.IBasicInformation;
using ERP.Models.BasicInformation;

namespace ERP.DataAccess.Repository.BasicInformation
{
    public class MyCompanyRepository : Repository<MyCompany>, IMyCompanyRepository
    {
        private ApplicationDbContext _db;
        public MyCompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MyCompany myCompany)
        {
            _db.MyCompany.Update(myCompany);
        }
    }
}
