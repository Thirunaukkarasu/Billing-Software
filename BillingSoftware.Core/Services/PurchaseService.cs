using Billing.Domain.Models;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Repository.Contracts;
using System;

namespace BillingSoftware.Core.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        private readonly IProductService _productService;
        public PurchaseService(IPurchaseRepository purchaseRepository, IProductService productService)
        {
            _purchaseRepository = purchaseRepository;
            _productService = productService;
        }

        public Guid SavePurchaseProducts(List<PurchasedProductsDto> productsDtos, Guid purchaseId)
        {

            var purchasedProducts = _purchaseRepository.GetPurchasedProduct(purchaseId).ToList();
            productsDtos.ForEach(x =>
            {
                Guid productId;
                if (x.ProductId == Guid.Empty)
                {
                    productId = _productService.SaveProduct(x.SelectedProduct);
                }
                else
                {
                    productId = _productService.UpdateProduct(x.SelectedProduct);
                }
                var purchasedProduct = purchasedProducts.Where(x => x.ProductId == productId && x.PurchaseId == purchaseId)
                                                        .FirstOrDefault();

                if (purchasedProduct != null)
                {
                    purchasedProduct.BatchNumber = x.BatchNumber;
                    purchasedProduct.HSNCode = x.HSNCode;
                    purchasedProduct.CategoryId = x.CategoryId;
                    purchasedProduct.DisplayName = x.DisplayName;
                    purchasedProduct.GSTPercent = x.GSTPercent;
                    purchasedProduct.CGSTPercent = x.CGSTPercent;
                    purchasedProduct.ProductDescription = x.Description;
                    purchasedProduct.ProductName = x.ProductName;
                    purchasedProduct.ProductSize = x.ProductSize;
                    purchasedProduct.Quantity = x.Quantity;
                    purchasedProduct.MRP = x.MRP; 
                    purchasedProduct.PurchaseRate = x.PurchaseRate;
                    purchasedProduct.PurchaseDiscountPercent = x.PurchaseDiscountPercent; 
                    purchasedProduct.SalesDiscountPercent = x.SalesDiscountPercent;
                    purchasedProduct.SalesRate = x.SalesRate;
                    purchasedProduct.PurchaseId = purchaseId;
                    purchasedProduct.ProductId = productId;
                    purchasedProduct.MeasurementUnitId = x.SelectedMeasurementUnit.MeasurementUnitId;
                    _purchaseRepository.UpdatePurchasedProducts(purchasedProduct, purchaseId);

                }
                else
                {
                    var purchasedNewProduct = new PurchasedProduct()
                    {
                        BatchNumber = x.BatchNumber,
                        HSNCode = x.HSNCode,
                        CategoryId = x.CategoryId,
                        DisplayName = x.DisplayName,
                        GSTPercent = x.GSTPercent,
                        CGSTPercent = x.CGSTPercent,
                        ProductDescription = x.Description,
                        MRP = x.MRP,
                        ProductName = x.ProductName,
                        ProductSize = x.ProductSize,
                        PurchaseDiscountPercent = x.PurchaseDiscountPercent,
                        Quantity = x.Quantity,
                        PurchaseRate = x.PurchaseRate,
                        SalesDiscountPercent = x.SalesDiscountPercent,
                        SalesRate = x.SalesRate,
                        PurchaseId = purchaseId,
                        ProductId = productId,
                        MeasurementUnitId = x.SelectedMeasurementUnit.MeasurementUnitId,
                    };
                    _purchaseRepository.AddPurchasedProducts(purchasedNewProduct, purchaseId);
                }
                
            });
            return purchaseId;
        }

        public List<PurchasedProductsDto> GetPurchasedProduct(Guid purchaseId)
        {
            var productsList = _productService.GetProducts();
            var products = _purchaseRepository.GetPurchasedProduct(purchaseId);
            if (products != null)
            {
                return products.Select(x => new PurchasedProductsDto
                {
                    ProductId = purchaseId,
                    ProductName = x.ProductName,
                    BatchNumber = x.BatchNumber,
                    HSNCode = x.HSNCode,
                    CategoryId = x.CategoryId,
                    DisplayName = x.DisplayName,
                    GSTPercent = x.GSTPercent,
                    CGSTPercent = x.CGSTPercent,
                    Description = x.ProductDescription,
                    MRP = x.MRP,
                    ProductSize = x.ProductSize,
                    PurchaseDiscountPercent = x.PurchaseDiscountPercent,
                    Quantity = x.Quantity,
                    PurchaseRate = x.PurchaseRate,
                    SalesDiscountPercent = x.SalesDiscountPercent,
                    SalesRate = x.SalesRate,
                    PurchaseId = x.PurchaseId,
                    ProductsDBSource = productsList,
                    SelectedProductCategoryRow = new Domain.Models.ProductCategoryDto()
                    {
                        CategoryId = x.Category.CategoryId,
                        CategoryName = x.Category.CategoryName,
                        LocalCategoryName = x.Category.LocalCategoryName
                    },
                    SelectedProduct = productsList.Where(y => y.ProductId == x.ProductId).FirstOrDefault(),
                    //CBProductsSource = productsList.Where(y => y.CategoryId == x.CategoryId).ToList().ToObservableCollection(),
                    SelectedMeasurementUnit = new Domain.Models.MeasurementUnitDto() { MeasurementUnitId = x.MeasurementUnitId, MeasurementUnitName = x.MeasurementUnit.MeasurementUnitName, Symbol = x.MeasurementUnit.Symbol },
                }).ToList();
            }
            else { return null; }
        }
    }
}
