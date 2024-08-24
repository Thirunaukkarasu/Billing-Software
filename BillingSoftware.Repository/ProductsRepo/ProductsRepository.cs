using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Billing.Repository.Imp.ProductsRepo
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly POSBillingDBContext _dbContext;
        public ProductsRepository(POSBillingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid SaveProductsDetails(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product.ProductId; 
        }

        //public ProductsCollection GetProductsItemsDetails(Guid invoiceId, Guid companyId)
        //{ 
        //    var products = dbContext.ProductItem.Where(x => x.InvoiceId == invoiceId).Select(x => new ProductItems() 
        //    {
        //        CompanyId = companyId,
        //        BatchNo = x.BatchNo,
        //        CategoryId = x.CategoryId,
        //        MeasurementUnitId = x.MeasurementUnitId,
        //        DiscountPercent = x.DiscountPercent.ToString(),
        //        DisplayName = x.DisplayName,
        //        GSTPercent = x.GSTPercent.ToString(),
        //        HSNCode = x.HSNCode,
        //        InvoiceId = invoiceId,
        //        MRP = x.MRP.ToString(),
        //        NoOfUnits = x.NoOfUnits.ToString(),
        //        ProductName = x.ProductName,
        //        ProductId= x.ProductId,
        //        PurchaseRate = x.PurchaseRate.ToString(),
        //        QuantityPerUnit = x.QuantityPerUnit.ToString(),
        //        SalesRate = x.SalesRate.ToString(), 
        //    }).ToList();

        //    ProductsCollection productCollection = new()
        //    {
        //        Products = products
        //    };
        //    return productCollection; 
        //}

        public IEnumerable<Product> GetProductsByCategory(Guid? CategoryId)
        {
            return _dbContext.Products.Include(x => x.Category).Include(x => x.MeasurementUnit).Where(x => x.CategoryId.Equals(CategoryId)).ToList(); 
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.Include(x => x.Category).Include(x => x.MeasurementUnit).AsEnumerable();
        }
        public  Product  GetProductsByProductId(Guid productId)
        {
            return _dbContext.Products.ToList().Where(x => x.ProductId == productId).FirstOrDefault(); 
        }
        public Guid UpdateProductsDetails(ProductsDto productsDto)
        {
            var product = _dbContext.Products.Where(x => x.ProductId == productsDto.ProductId).FirstOrDefault();
            if (product != null)
            { 
                product.HSNCode = productsDto.HSNCode;
                product.BatchNumber = productsDto.BatchNumber;
                product.CategoryId = productsDto.CategoryId;
                product.DisplayName = productsDto.DisplayName;
                product.GSTPercent = productsDto.GSTPercent;
                product.CGSTPercent = productsDto.CGSTPercent;
                product.ProductDescription = productsDto.Description;
                product.MRP = productsDto.MRP;
                product.ProductName = productsDto.ProductName;
                product.ProductSize = productsDto.ProductSize;
                product.PurchaseDiscountPercent = productsDto.PurchaseDiscountPercent;
                product.Quantity = productsDto.Quantity;
                product.PurchaseRate = productsDto.PurchaseRate;
                product.SalesDiscountPercent = productsDto.SalesDiscountPercent;
                product.SalesRate = productsDto.SalesRate;
                product.MeasurementUnitId = productsDto.SelectedMeasurementUnit.MeasurementUnitId;
                product.ModifiedDate = DateTime.Now;
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges(); 
            }
            return product.ProductId;
        }

      
    }
}
