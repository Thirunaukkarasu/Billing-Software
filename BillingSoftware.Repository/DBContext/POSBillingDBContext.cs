using Microsoft.EntityFrameworkCore;
using Billing.Domain.Models;
using BillingSoftware.Domain.Models;

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
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<ProductItems> ProductItems { get; set; }
        public virtual DbSet<CompanyDetails> CompanyDetails { get; set; }
        public virtual DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        #endregion

        #region Overridden method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<ProductItems>();
            modelBuilder.Entity<CompanyDetails>();
            modelBuilder.Entity<InvoiceDetails>();
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
