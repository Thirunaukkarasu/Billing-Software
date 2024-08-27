using BillingSoftware.Domain.Entities;
using BillingSoftware.Domain.Models;
using System;

namespace Billing.Domain.Models
{
    public class ProductsDto  
    {
         
        private string productName;
        private string _description;
        private string displayName;
        private string hSNCode;
        private string batchNumber;
        private decimal mRP;
        private int quantity;
        private decimal productSize;
        private decimal gSTPercent;
        private decimal cGSTPercent;
        private decimal purchaseDiscountPercent;
        private decimal purchaseRate;
        private decimal salesDiscountPercent;
        private decimal salesRate; 
        private Guid categoryId;
        private int stocks;
        private Guid productId;

        public Guid ProductId { get => productId; set => productId = value; }
     
        public ProductCategoryDto Category { get; set; }

        public int Stocks { get => stocks; set => stocks = value; }
        
        public MeasurementUnitDto MeasurementUnit { get; set; }
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
                //OnPropertyChanged(nameof(ProductName));
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                //OnPropertyChanged(nameof(Description));
            }
        }
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
                //OnPropertyChanged(nameof(DisplayName));
            }
        }
        public string HSNCode
        {
            get
            {
                return hSNCode;
            }
            set
            {
                hSNCode = value;
                //OnPropertyChanged(nameof(HSNCode));
            }
        }
        public string BatchNumber
        {
            get
            {
                return batchNumber;
            }
            set
            {
                batchNumber = value;
                //OnPropertyChanged(nameof(BatchNumber));
            }
        }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                //OnPropertyChanged(nameof(Quantity));
            }
        }
        public decimal ProductSize
        {
            get
            {
                return productSize;
            }

            set
            {
                productSize = value;
                //OnPropertyChanged(nameof(ProductSize));
            }
        }
        public decimal MRP
        {
            get
            {
                return mRP;
            }

            set
            {
                mRP = value;
                //OnPropertyChanged(nameof(MRP));
            }
        }
        public decimal GSTPercent
        {
            get
            {
                return gSTPercent;
            }

            set
            {
                gSTPercent = value;
                //OnPropertyChanged(nameof(GSTPercent));
            }
        }
        public decimal CGSTPercent
        {
            get
            {
                return cGSTPercent;
            }

            set
            {
                cGSTPercent = value;
                 
            }
        }
        public decimal PurchaseDiscountPercent
        {
            get
            {
                return purchaseDiscountPercent;
            }

            set
            {
                purchaseDiscountPercent = value;
                 
            }
        }
        public decimal PurchaseRate
        {
            get
            {
                return purchaseRate;
            }

            set
            {
                purchaseRate = value;
                
            }
        }
        public decimal SalesRate
        {
            get
            {
                return salesRate;
            }

            set
            {
                salesRate = value;
                
            }
        }

        public decimal SalesDiscountPercent
        {
            get
            {
                return salesDiscountPercent;
            }

            set
            {
                salesDiscountPercent = value;
                
            }
        } 
        public Guid CategoryId
        {
            get
            {
                return categoryId;
            }

            set
            {
                categoryId = value;
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }
          
        #region Override Methods
        public override bool Equals(object obj)
        {
            if (obj is ProductsDto productsDto)
            {
                return ProductId == productsDto.ProductId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ProductId.GetHashCode();
        }
        #endregion Override Methods
    }
}
