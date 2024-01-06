using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;

namespace Billing.Repository.Imp.ProductsRepo
{
    public class ProductsRepository : IProductsRepository
    {
        private POSBillingDBContext dbContext;
        public ProductsRepository(POSBillingDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool SaveProductsDetails(ProductItems userDetails)
        {
            dbContext.ProductItems.Add(userDetails);
            var rowAffected = dbContext.SaveChanges();
            return rowAffected > 0 ? true : false;
        }
    }
}
