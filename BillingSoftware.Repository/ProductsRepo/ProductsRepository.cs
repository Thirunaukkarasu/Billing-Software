using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ProductsCollection GetProductsItemsDetails(Guid invoiceId, Guid companyId)
        { 
            var productCollection = new ProductsCollection
            {
                Products = dbContext.ProductItems.Where(x => x.InvoiceId == invoiceId && x.CompanyId == companyId).ToList()
            };
            return productCollection;
        }
    }
}
