using Billing.Domain.Models;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;

namespace BillingSoftware.Core.Contracts
{
    public interface IPurchaseService
    {
        Guid SavePurchaseProducts(List<PurchasedProductsDto> productsDtos, Guid purchaseId);
        List<PurchasedProductsDto> GetPurchasedProduct(Guid purchaseId);
    }
}
