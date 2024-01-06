using Billing.Domain.Models;

namespace Billing.Repository.Contracts
{
    public interface IProductsRepository
    {
        bool SaveProductsDetails(ProductItems userDetails);
    }
}
