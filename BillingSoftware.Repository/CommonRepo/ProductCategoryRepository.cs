using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingSoftware.Repository.CommonRepo
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly POSBillingDBContext dbContext;
        public ProductCategoryRepository(POSBillingDBContext dbContext)
        {
            this.dbContext = dbContext;
        } 

        public List<ProductCategoryDto> GetProductCategory()
        { 
           return dbContext.ProductCategory.Select(x => new ProductCategoryDto()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName 
            }).ToList();
        }

        public Guid SaveProductCategory(ProductCategoryDto productCategory)
        { 
            var category = new ProductCategory() 
            {
               CategoryName = productCategory.CategoryName
            };

            dbContext.ProductCategory.Add(category);
            dbContext.SaveChanges();
            return category.CategoryId;
        }


    }
}
