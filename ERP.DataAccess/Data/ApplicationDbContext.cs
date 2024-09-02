﻿using ERP.Models.Purchase;
using Microsoft.EntityFrameworkCore;

namespace ERP.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
