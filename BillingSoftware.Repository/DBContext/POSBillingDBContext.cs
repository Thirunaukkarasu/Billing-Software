using Microsoft.EntityFrameworkCore;
using Billing.Domain.Models;
using BillingSoftware.Domain.Models;
using BillingSoftware.Domain.Entities;

namespace Billing.Repository.Imp.DBContext
{
    public class POSBillingDBContext : DbContext
    {
        #region Contructor
        public POSBillingDBContext(DbContextOptions<POSBillingDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        #endregion

        #region Public properties
        public DbSet<User> User { get; set; }
        public DbSet<ProductItem> ProductItem { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductMeasurementUnit> ProductMeasurementUnit { get; set; } 
        #endregion

        #region Overridden method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<ProductItem>();
            modelBuilder.Entity<Company>();
            modelBuilder.Entity<Invoice>();
            modelBuilder.Entity<ProductCategory>();
            modelBuilder.Entity<ProductMeasurementUnit>();
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
