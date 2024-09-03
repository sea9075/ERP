using ERP.Models.Purchase;
using Microsoft.EntityFrameworkCore;

namespace ERP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        DbSet<Supplier> Suppliers { get; set; }
    }
}
