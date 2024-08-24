using Billing.Domain.Models;
using BillingSoftware.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface IPurchaseRepository
    {
        Guid SavePurchasedProducts(PurchasedProduct purchasedProduct);

        IEnumerable<PurchasedProduct> GetPurchasedProduct(Guid purchaseId);
    }
}
