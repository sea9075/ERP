using ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
