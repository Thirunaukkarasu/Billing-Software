using Billing.Domain.Models;
using Billing.Repository.Contracts;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using System;

namespace BillingSoftware.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Guid SaveProduct(ProductsDto productsDto)
        {
            var product = new Product()
            {
                HSNCode = productsDto.HSNCode,
                BatchNumber = productsDto.BatchNumber,
                CategoryId = productsDto.CategoryId,
                DisplayName = productsDto.DisplayName,
                GSTPercent = productsDto.GSTPercent,
                CGSTPercent = productsDto.CGSTPercent,
                ProductDescription = productsDto.Description,
                MRP = productsDto.MRP,
                ProductName = productsDto.ProductName,
                ProductSize = productsDto.ProductSize,
                PurchaseDiscountPercent = productsDto.PurchaseDiscountPercent,
                Quantity = productsDto.Quantity,
                PurchaseRate = productsDto.PurchaseRate,
                SalesDiscountPercent = productsDto.SalesDiscountPercent,
                SalesRate = productsDto.SalesRate
            };
            return _productsRepository.SaveProductsDetails(product);
        }

        public Guid UpdateProduct(ProductsDto productsDto)
        {
            Guid guid = productsDto.ProductId ?? Guid.Empty;
            return _productsRepository.UpdateProductsDetails(productsDto);
            //var product = _productsRepository.GetProductsByProductId(guid);
            //if (product != null)
            //{
            //    product.ProductId = guid;
            //    product.HSNCode = productsDto.HSNCode;
            //    product.BatchNumber = productsDto.BatchNumber;
            //    product.CategoryId = productsDto.CategoryId;
            //    product.DisplayName = productsDto.DisplayName;
            //    product.GSTPercent = productsDto.GSTPercent;
            //    product.CGSTPercent = productsDto.CGSTPercent;
            //    product.ProductDescription = productsDto.Description;
            //    product.MRP = productsDto.MRP;
            //    product.ProductName = productsDto.ProductName;
            //    product.ProductSize = productsDto.ProductSize;
            //    product.PurchaseDiscountPercent = productsDto.PurchaseDiscountPercent;
            //    product.Quantity = productsDto.Quantity;
            //    product.PurchaseRate = productsDto.PurchaseRate;
            //    product.SalesDiscountPercent = productsDto.SalesDiscountPercent;
            //    product.SalesRate = productsDto.SalesRate;
            //    product.MeasurementUnitId = productsDto.SelectedMeasurementUnit.MeasurementUnitId; 
            //    product.ModifiedDate = DateTime.Now;
            //    return _productsRepository.UpdateProductsDetails(product);
            //}
            //return product.ProductId;
        }

        public List<ProductsDto> GetProductsByCategory(Guid? CategoryId)
        {
            var products = _productsRepository.GetProductsByCategory(CategoryId);
            return GetProductsDtos(products);

        }

        public List<ProductsDto> GetProducts()
        {
            var products = _productsRepository.GetProducts();
            return GetProductsDtos(products);
        }

        private List<ProductsDto> GetProductsDtos(IEnumerable<Product> products)
        {
            return products.Select(x => new ProductsDto()
            {
                HSNCode = x.HSNCode,
                BatchNumber = x.BatchNumber,
                CategoryId = x.CategoryId,
                SelectedProductCategoryRow = new ProductCategoryDto()
                {
                    CategoryId = x.Category.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    TamilCategoryName = x.Category.CategoryName,
                },
                DisplayName = x.DisplayName,
                GSTPercent = x.GSTPercent,
                CGSTPercent = x.CGSTPercent,
                Description = x.ProductDescription,
                MRP = x.MRP,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductSize = x.ProductSize,
                PurchaseDiscountPercent = x.PurchaseDiscountPercent,
                Quantity = x.Quantity,
                PurchaseRate = x.PurchaseRate,
                SalesDiscountPercent = x.SalesDiscountPercent,
                SalesRate = x.SalesRate,
                SelectedMeasurementUnit = new MeasurementUnitDto()
                {
                    MeasurementUnitId = x.MeasurementUnit.MeasurementUnitId,
                    MeasurementUnitName = x.MeasurementUnit.MeasurementUnitName,
                    Symbol = x.MeasurementUnit.Symbol,
                },

            }).ToList();
        }

        //public Result<ProductsCollection> GetProductsItemsDetails(Guid invoiceId, Guid companyId)
        //{
        //    try
        //    {
        //        //var result = _productsRepository.GetProductsItemsDetails(invoiceId, companyId);
        //        return result == null ? Result.Fail<ProductsCollection>("Unable to save product items. Please try again") : Result.Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return Result.Fail<ProductsCollection>("Error while reading " + e.Message);
        //    } 
        //}

        //public Result<bool> SaveProductItems(ProductsDto productItems)
        //{

        //    try
        //    {
        //        //var result = _productsRepository.SaveProductsDetails(productItems);
        //        return !result ? Result.Fail<bool>("Unable to save product items. Please try again") : Result.Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return Result.Fail<bool>("Error while reading " + e.Message);
        //    }
        //}
    }
}
