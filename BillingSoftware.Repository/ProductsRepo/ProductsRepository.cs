using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;

namespace Billing.Repository.Imp.ProductsRepo
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly POSBillingDBContext dbContext;
        public ProductsRepository(POSBillingDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool SaveProductsDetails(ProductItems productItems)
        {
            dbContext.ProductItems.Add(productItems);
            var rowAffected = dbContext.SaveChanges();
            return rowAffected > 0;
        }
    }
}
