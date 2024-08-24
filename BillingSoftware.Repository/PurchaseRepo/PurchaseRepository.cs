using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingSoftware.Repository.PurchaseRepo
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly POSBillingDBContext _dbContext;
        private readonly IProductsRepository _productsRepository;
        public PurchaseRepository(POSBillingDBContext dbContext, IProductsRepository productsRepository)
        {
            _dbContext = dbContext;
            _productsRepository = productsRepository;
        }


        public Guid SavePurchasedProducts(PurchasedProduct purchasedProduct)
        {
            var invoice = _dbContext.Invoice.Where(x => x.PurchaseId == purchasedProduct.PurchaseId).FirstOrDefault();

            if (invoice != null)
            { 
                _dbContext.PurchasedProduct.Add(purchasedProduct);
                _dbContext.SaveChanges(); 
            } 
            return invoice.PurchaseId;
        }

        public IEnumerable<PurchasedProduct> GetPurchasedProduct(Guid purchaseId)
        {
            return _dbContext.PurchasedProduct
                     .Where(x => x.PurchaseId == purchaseId).AsEnumerable(); 
        }
    }
}
