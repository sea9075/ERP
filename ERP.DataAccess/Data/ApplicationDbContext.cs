using ERP.Models.BasicInformation;
using Microsoft.EntityFrameworkCore;

namespace ERP.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MyCompany> MyCompany { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
