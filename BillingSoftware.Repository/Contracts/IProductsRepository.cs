using Billing.Domain.Models;
using System.Collections.Generic;
using System;
using BillingSoftware.Domain.Entities;

namespace Billing.Repository.Contracts
{
    public interface IProductsRepository
    {
        Guid SaveProductsDetails(Product product);
         

        IEnumerable<Product> GetProductsByCategory(Guid? CategoryId);

        IEnumerable<Product> GetProducts();
        Product GetProductsByProductId(Guid productId);
        Guid UpdateProductsDetails(ProductsDto productsDto);

        //bool SaveProductsDetails(ProductItems productItems);
        //ProductsCollection GetProductsItemsDetails(Guid invoiceId, Guid companyId);
    }
}
