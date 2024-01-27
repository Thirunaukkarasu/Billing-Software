using Billing.Repository.Imp.DBContext;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Repository.CommonRepo
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly POSBillingDBContext dbContext;
        public ProductCategoryRepository(POSBillingDBContext dbContext)
        {
            this.dbContext = dbContext;
        } 

        public List<ProductItemCategory> GetProductItemCategory()
        { 
           return dbContext.ProductCategory.Select(x => new ProductItemCategory()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName 
            }).ToList();
        }
    }
}
