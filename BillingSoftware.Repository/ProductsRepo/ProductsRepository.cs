using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using System;
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
            if (productItems.ProductId != null)
            {
                var productItm = dbContext.ProductItem.Where(x => x.ProductId == productItems.ProductId).FirstOrDefault();
                if (productItm != null)
                {
                    productItm.BatchNo = productItems.BatchNo;
                    productItm.CategoryId = productItems.CategoryId;
                    productItm.MeasurementUnitId = productItems.MeasurementUnitId;
                    productItm.DisplayName = productItems.DisplayName;
                    productItm.QuantityPerUnit = productItems.QuantityPerUnit;
                    productItm.DiscountPercent = Convert.ToDecimal(productItems.DiscountPercent);
                    productItm.GSTPercent = Convert.ToDecimal(productItems.GSTPercent);
                    productItm.HSNCode = productItems.HSNCode;
                    productItm.ModifiedDt = DateTime.Now;
                    productItm.MRP = Convert.ToDecimal(productItems.MRP);
                    productItm.NoOfUnits = Convert.ToInt32(productItems.NoOfUnits);
                    productItm.ProductName = productItems.ProductName;
                    productItm.PurchaseRate = Convert.ToDecimal(productItems.PurchaseRate);
                    productItm.SalesRate = Convert.ToDecimal(productItems.SalesRate);
                }
                dbContext.ProductItem.Update(productItm);
            }
            else
            {
                var product = new ProductItem()
                {
                    InvoiceId = productItems.InvoiceId,
                    BatchNo = productItems.BatchNo,
                    CategoryId = productItems.CategoryId,
                    DisplayName = productItems.DisplayName,
                    QuantityPerUnit = productItems.QuantityPerUnit,
                    DiscountPercent = Convert.ToDecimal(productItems.DiscountPercent),
                    GSTPercent = Convert.ToDecimal(productItems.GSTPercent),
                    HSNCode = productItems.HSNCode,
                    InsertedDt = DateTime.Now,
                    ModifiedDt = null,
                    MRP = Convert.ToDecimal(productItems.MRP),
                    NoOfUnits = Convert.ToInt32(productItems.NoOfUnits),
                    ProductName = productItems.ProductName,
                    PurchaseRate = Convert.ToDecimal(productItems.PurchaseRate),
                    SalesRate = Convert.ToDecimal(productItems.SalesRate),
                    MeasurementUnitId = productItems.MeasurementUnitId,
                };

                dbContext.ProductItem.Add(product);
            }
            var rowAffected = dbContext.SaveChanges();
            return rowAffected > 0;
        }

        public ProductsCollection GetProductsItemsDetails(Guid invoiceId, Guid companyId)
        { 
            var products = dbContext.ProductItem.Where(x => x.InvoiceId == invoiceId).Select(x => new ProductItems() 
            {
                CompanyId = companyId,
                BatchNo = x.BatchNo,
                CategoryId = x.CategoryId,
                MeasurementUnitId = x.MeasurementUnitId,
                DiscountPercent = x.DiscountPercent.ToString(),
                DisplayName = x.DisplayName,
                GSTPercent = x.GSTPercent.ToString(),
                HSNCode = x.HSNCode,
                InvoiceId = invoiceId,
                MRP = x.MRP.ToString(),
                NoOfUnits = x.NoOfUnits.ToString(),
                ProductName = x.ProductName,
                ProductId= x.ProductId,
                PurchaseRate = x.PurchaseRate.ToString(),
                QuantityPerUnit = x.QuantityPerUnit.ToString(),
                SalesRate = x.SalesRate.ToString(), 
            }).ToList();

            ProductsCollection productCollection = new()
            {
                Products = products
            };
            return productCollection; 
        }
    }
}
