using Billing.Domain.Models;
using Billing.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts
{
    public interface IProductService
    {
        Result<bool> SaveProductItems(ProductItems productItems);

        Result<ProductsCollection> GetProductsItemsDetails(Guid invoiceId, Guid companyId);
    }
}
