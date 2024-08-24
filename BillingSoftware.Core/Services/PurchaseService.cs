using Billing.Domain.Models;
using Billing.Repository.Contracts;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Entities;
using BillingSoftware.Repository.Contracts;

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

        public Guid SavePurchaseProducts(List<ProductsDto> productsDtos, Guid purchaseId)
        {
            productsDtos.ForEach(x =>
            {
                Guid productId;
                if (x.ProductId == Guid.Empty || x.ProductId == null)
                {
                    productId = _productService.SaveProduct(x);
                }
                else
                {
                    productId = _productService.UpdateProduct(x);
                }

                var purchasedProduct = new PurchasedProduct()
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
                _purchaseRepository.SavePurchasedProducts(purchasedProduct);
            });
            return purchaseId;
        }

        public List<ProductsDto> GetPurchasedProduct(Guid purchaseId) 
        {
            var products = _purchaseRepository.GetPurchasedProduct(purchaseId);
            if (products != null)
            {
              return  products.Select(x => new ProductsDto
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
                    PurchaseId = x.PurchaseId
                }).ToList(); 
            }
            else {  return null; }  
        } 
    }
}
