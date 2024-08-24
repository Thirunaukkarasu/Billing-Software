using Billing.Domain.Models;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;

namespace BillingSoftware.Core.Contracts
{
    public interface IPurchaseService
    {
        Guid SavePurchaseProducts(List<ProductsDto> productsDtos, Guid purchaseId);
        List<ProductsDto> GetPurchasedProduct(Guid purchaseId);
    }
}
