using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
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


        public Guid AddPurchasedProducts(PurchasedProduct purchasedProduct, Guid PurchaseId)
        {
            var invoice = _dbContext.Invoice.Where(x => x.PurchaseId == PurchaseId).FirstOrDefault();

            if (invoice != null)
            { 
                _dbContext.PurchasedProduct.Add(purchasedProduct);
                _dbContext.SaveChanges(); 
            } 
            return invoice.PurchaseId;
        }

        public void UpdatePurchasedProducts(PurchasedProduct purchasedProduct, Guid PurchaseId)
        {
            var existingProduct = _dbContext.PurchasedProduct.Where(x => x.PurchaseId == PurchaseId && x.ProductId == purchasedProduct.ProductId).FirstOrDefault();

            if (existingProduct != null)
            {
                _dbContext.PurchasedProduct.Update(purchasedProduct);
                _dbContext.SaveChanges();
            } 
        }

        public IEnumerable<PurchasedProduct> GetPurchasedProduct(Guid purchaseId)
        {
            return _dbContext.PurchasedProduct
                             .Include(x => x.Category)
                             .Include(y => y.MeasurementUnit)
                             .Where(x => x.PurchaseId == purchaseId)
                             .AsEnumerable(); 
        }
    }
}
