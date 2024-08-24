using BillingSoftware.Domain.Models;
using System;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface IProductCategoryRepository
    {
        public List<ProductCategoryDto> GetProductCategory();

        Guid SaveProductCategory(ProductCategoryDto productCategory);
    }
}
