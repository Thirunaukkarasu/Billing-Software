using Billing.Domain.Models;
using System.Collections.Generic;
using System;

namespace Billing.Repository.Contracts
{
    public interface IProductsRepository
    {
        bool SaveProductsDetails(ProductItems productItems);
        ProductsCollection GetProductsItemsDetails(Guid invoiceId, Guid companyId);
    }
}
