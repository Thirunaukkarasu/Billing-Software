using Billing.Domain.Models;
using Billing.Domain.Results;
using Billing.Repository.Contracts;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Models;
using BillingSoftware.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Result<ProductsCollection> GetProductsItemsDetails(Guid invoiceId, Guid companyId)
        {
            try
            {
                var result = _productsRepository.GetProductsItemsDetails(invoiceId, companyId);
                return result == null ? Result.Fail<ProductsCollection>("Unable to save product items. Please try again") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<ProductsCollection>("Error while reading " + e.Message);
            } 
        }

        public Result<bool> SaveProductItems(ProductItems productItems)
        {

            try
            {
                var result = _productsRepository.SaveProductsDetails(productItems);
                return !result ? Result.Fail<bool>("Unable to save product items. Please try again") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<bool>("Error while reading " + e.Message);
            }
        }
    }
}
