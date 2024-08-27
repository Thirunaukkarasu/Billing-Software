using Billing.Domain.Models;
using BillingSoftware.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BillingSoftware.Repository.Contracts
{
    public interface IPurchaseRepository
    {
        Guid AddPurchasedProducts(PurchasedProduct purchasedProduct, Guid PurchaseId);

        void UpdatePurchasedProducts(PurchasedProduct purchasedProduct, Guid PurchaseId);

        IEnumerable<PurchasedProduct> GetPurchasedProduct(Guid purchaseId);
    }
}
