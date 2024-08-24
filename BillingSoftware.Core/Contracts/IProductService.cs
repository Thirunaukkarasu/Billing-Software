using Billing.Domain.Models;
using BillingSoftware.Domain.Entities;

namespace BillingSoftware.Core.Contracts
{
    public interface IProductService
    {
        Guid SaveProduct(ProductsDto productsDto);

        Guid UpdateProduct(ProductsDto productsDto);

        List<ProductsDto> GetProductsByCategory(Guid? CategoryId);

        List<ProductsDto> GetProducts();

        //Result<bool> SaveProductItems(ProductsDto productItems);

        //Result<ProductsCollection> GetProductsItemsDetails(Guid invoiceId, Guid companyId);
    }
}
